using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Bravito.Application.Knowledge.DTOs;
using Bravito.Application.Knowledge.Interfaces;
using Bravito.Domain.Knowledge.Entities;
using Bravito.Domain.Knowledge.Enums;
using Microsoft.Extensions.Configuration;

namespace Bravito.Application.Knowledge.Services;

public class KnowledgeDocumentService : IKnowledgeDocumentService
{
    private readonly IKnowledgeDocumentRepository _documentRepository;
    private readonly IVectorDocumentRepository _vectorRepository;
    private readonly IFileStorageService _storageService;
    private readonly ITextChunkingService _chunkingService;
    private readonly IEmbeddingService _embeddingService;
    private readonly IEnumerable<ITextExtractionService> _extractionServices;
    private readonly IConfiguration _configuration;

    public KnowledgeDocumentService(
        IKnowledgeDocumentRepository documentRepository,
        IVectorDocumentRepository vectorRepository,
        IFileStorageService storageService,
        ITextChunkingService chunkingService,
        IEmbeddingService embeddingService,
        IEnumerable<ITextExtractionService> extractionServices,
        IConfiguration configuration)
    {
        _documentRepository = documentRepository;
        _vectorRepository = vectorRepository;
        _storageService = storageService;
        _chunkingService = chunkingService;
        _embeddingService = embeddingService;
        _extractionServices = extractionServices;
        _configuration = configuration;
    }

    public async Task<HealthResponse> CheckHealthAsync(CancellationToken cancellationToken = default)
    {
        var response = new HealthResponse { Success = true };

        try
        {
            var hasKnowledgeDbStr = !string.IsNullOrWhiteSpace(_configuration.GetConnectionString("KnowledgeDb"));
            if (!hasKnowledgeDbStr) throw new Exception("ConnectionStrings__KnowledgeDb is missing.");
            
            response.Database = await _vectorRepository.CheckDatabaseConnectionAsync(cancellationToken) ? "ok" : "fail";
            if (response.Database == "fail") throw new Exception("Cannot connect to Database.");

            var schema = _configuration["KNOWLEDGE_DB_SCHEMA"] ?? "public";
            var vectorTable = _configuration["KNOWLEDGE_VECTOR_TABLE"] ?? "documents";

            response.KnowledgeDocumentsTable = await _vectorRepository.CheckTableExistsAsync("knowledge_documents", cancellationToken) ? "ok" : "fail";
            response.DocumentsTable = await _vectorRepository.CheckTableExistsAsync(vectorTable, cancellationToken) ? "ok" : "fail";

            response.StoragePathValue = _storageService.GetStoragePath();
            response.StoragePath = _storageService.CheckStoragePathWritable() ? "ok" : "fail";

            response.GeminiApiKey = string.IsNullOrWhiteSpace(_configuration["GEMINI_API_KEY"]) ? "missing" : "ok";
            response.EmbeddingModel = string.IsNullOrWhiteSpace(_configuration["KNOWLEDGE_EMBEDDING_MODEL"]) ? "missing" : "ok";

            if (response.KnowledgeDocumentsTable == "fail" || response.DocumentsTable == "fail" || response.StoragePath == "fail" || response.GeminiApiKey == "missing" || response.EmbeddingModel == "missing")
            {
                response.Success = false;
                response.ErrorMessage = "One or more health checks failed.";
            }
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.ErrorMessage = ex.Message;
        }

        return response;
    }

    private string GetMimeType(string fileName)
    {
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        return ext switch
        {
            ".pdf" => "application/pdf",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".txt" => "text/plain",
            _ => "application/octet-stream"
        };
    }

    private ITextExtractionService GetExtractionService(string ext)
    {
        foreach (var svc in _extractionServices)
        {
            if (ext == ".pdf" && svc.GetType().Name.Contains("Pdf")) return svc;
            if (ext == ".docx" && svc.GetType().Name.Contains("Docx")) return svc;
            if (ext == ".txt" && svc.GetType().Name.Contains("Txt")) return svc;
        }
        throw new NotSupportedException($"Unsupported extension {ext}");
    }

    private string CalculateHash(Stream stream)
    {
        using var sha256 = SHA256.Create();
        stream.Position = 0;
        var hashBytes = sha256.ComputeHash(stream);
        stream.Position = 0;
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
    }

    private async Task ProcessDocumentInnerAsync(KnowledgeDocument doc, CancellationToken cancellationToken)
    {
        var ext = Path.GetExtension(doc.FileName).ToLowerInvariant();
        var extractionService = GetExtractionService(ext);

        var text = await extractionService.ExtractTextAsync(doc.FilePath, doc.MimeType, cancellationToken);
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new InvalidOperationException("Extracted text is empty.");
        }

        var chunks = _chunkingService.ChunkText(text);
        if (chunks.Count == 0)
        {
            throw new InvalidOperationException("No chunks generated from text.");
        }

        int chunkIndex = 0;
        foreach (var chunk in chunks)
        {
            var embedding = await _embeddingService.GenerateEmbeddingAsync(chunk, cancellationToken);
            
            var metadata = new
            {
                document_id = doc.Id.ToString(),
                file_name = doc.FileName,
                app = doc.App,
                uploaded_at = doc.UploadedAt.ToString("O"),
                chunk_index = chunkIndex,
                source = "upload",
                file_hash = doc.FileHash
            };
            
            var metadataJson = JsonSerializer.Serialize(metadata);

            await _vectorRepository.InsertChunkAsync(Guid.NewGuid(), chunk, metadataJson, embedding, cancellationToken);
            chunkIndex++;
        }

        doc.ChunkCount = chunks.Count;
        doc.ProcessedAt = DateTime.UtcNow;
        doc.Status = KnowledgeDocumentStatus.Processed.ToString().ToLowerInvariant();
        doc.ErrorMessage = null;
    }

    public async Task<UploadResponse> UploadAsync(Stream fileStream, string fileName, string? app, CancellationToken cancellationToken = default)
    {
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        var allowedExts = (_configuration["KNOWLEDGE_ALLOWED_EXTENSIONS"] ?? ".pdf,.docx,.txt").Split(',');
        if (Array.IndexOf(allowedExts, ext) < 0)
            throw new Exception($"Invalid extension. Allowed: {string.Join(", ", allowedExts)}");

        var maxMb = int.TryParse(_configuration["KNOWLEDGE_MAX_FILE_SIZE_MB"], out var m) ? m : 25;
        if (fileStream.Length > maxMb * 1024 * 1024)
            throw new Exception($"File size exceeds {maxMb}MB limit.");

        var docId = Guid.NewGuid();
        var hash = CalculateHash(fileStream);

        var filePath = await _storageService.SaveFileAsync(fileStream, fileName, docId.ToString(), cancellationToken);

        var doc = new KnowledgeDocument
        {
            Id = docId,
            App = string.IsNullOrWhiteSpace(app) ? (_configuration["KNOWLEDGE_DEFAULT_APP"] ?? "bravito") : app,
            FileName = fileName,
            FilePath = filePath,
            FileHash = hash,
            MimeType = GetMimeType(fileName),
            FileSize = fileStream.Length,
            Status = KnowledgeDocumentStatus.Processing.ToString().ToLowerInvariant(),
            UploadedAt = DateTime.UtcNow
        };

        await _documentRepository.BeginTransactionAsync(cancellationToken);
        try
        {
            await _documentRepository.AddAsync(doc, cancellationToken);
            
            await ProcessDocumentInnerAsync(doc, cancellationToken);

            await _documentRepository.UpdateAsync(doc, cancellationToken);
            await _documentRepository.CommitTransactionAsync(cancellationToken);

            return new UploadResponse
            {
                Id = doc.Id,
                FileName = doc.FileName,
                Status = doc.Status,
                Message = "Documento processado com sucesso."
            };
        }
        catch (Exception ex)
        {
            await _documentRepository.RollbackTransactionAsync(cancellationToken);
            
            doc.Status = KnowledgeDocumentStatus.Error.ToString().ToLowerInvariant();
            doc.ErrorMessage = ex.Message;
            await _documentRepository.AddAsync(doc, cancellationToken); // Salva o erro sem os chunks

            throw new Exception($"Erro ao processar documento: {ex.Message}", ex);
        }
    }

    public async Task<DeleteResponse> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var doc = await _documentRepository.GetByIdAsync(id, cancellationToken);
        if (doc == null || doc.DeletedAt != null)
            throw new Exception("Documento não encontrado.");

        await _documentRepository.BeginTransactionAsync(cancellationToken);
        try
        {
            int deletedChunks = await _vectorRepository.DeleteChunksByDocumentIdAsync(id, cancellationToken);

            doc.Status = KnowledgeDocumentStatus.Deleted.ToString().ToLowerInvariant();
            doc.DeletedAt = DateTime.UtcNow;

            await _documentRepository.UpdateAsync(doc, cancellationToken);
            await _documentRepository.CommitTransactionAsync(cancellationToken);

            try { await _storageService.DeleteFileAsync(doc.FilePath); } catch { /* ignore if fails to delete physically */ }

            return new DeleteResponse
            {
                Success = true,
                Message = "Documento excluído da base de conhecimento.",
                DeletedChunks = deletedChunks
            };
        }
        catch
        {
            await _documentRepository.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    public async Task<ReplaceResponse> ReplaceAsync(Guid id, Stream fileStream, string fileName, CancellationToken cancellationToken = default)
    {
        var doc = await _documentRepository.GetByIdAsync(id, cancellationToken);
        if (doc == null || doc.DeletedAt != null)
            throw new Exception("Documento não encontrado.");

        var hash = CalculateHash(fileStream);

        await _documentRepository.BeginTransactionAsync(cancellationToken);
        try
        {
            int deletedOld = await _vectorRepository.DeleteChunksByDocumentIdAsync(id, cancellationToken);
            
            try { await _storageService.DeleteFileAsync(doc.FilePath); } catch { }

            var newFilePath = await _storageService.SaveFileAsync(fileStream, fileName, doc.Id.ToString(), cancellationToken);

            doc.FileName = fileName;
            doc.FilePath = newFilePath;
            doc.FileHash = hash;
            doc.MimeType = GetMimeType(fileName);
            doc.FileSize = fileStream.Length;
            doc.Status = KnowledgeDocumentStatus.Processing.ToString().ToLowerInvariant();
            doc.UploadedAt = DateTime.UtcNow; // reset update time
            
            await ProcessDocumentInnerAsync(doc, cancellationToken);

            await _documentRepository.UpdateAsync(doc, cancellationToken);
            await _documentRepository.CommitTransactionAsync(cancellationToken);

            return new ReplaceResponse
            {
                Success = true,
                Id = doc.Id,
                Message = "Documento substituído com sucesso.",
                DeletedOldChunks = deletedOld,
                NewChunks = doc.ChunkCount
            };
        }
        catch (Exception ex)
        {
            await _documentRepository.RollbackTransactionAsync(cancellationToken);
            
            doc.Status = KnowledgeDocumentStatus.Error.ToString().ToLowerInvariant();
            doc.ErrorMessage = ex.Message;
            await _documentRepository.UpdateAsync(doc, cancellationToken);
            
            throw new Exception($"Erro ao substituir documento: {ex.Message}", ex);
        }
    }

    public async Task<ReprocessResponse> ReprocessAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var doc = await _documentRepository.GetByIdAsync(id, cancellationToken);
        if (doc == null || doc.DeletedAt != null)
            throw new Exception("Documento não encontrado.");

        if (!await _storageService.FileExistsAsync(doc.FilePath))
            throw new Exception("Arquivo físico não encontrado para reprocessamento.");

        await _documentRepository.BeginTransactionAsync(cancellationToken);
        try
        {
            await _vectorRepository.DeleteChunksByDocumentIdAsync(id, cancellationToken);
            
            doc.Status = KnowledgeDocumentStatus.Processing.ToString().ToLowerInvariant();
            await ProcessDocumentInnerAsync(doc, cancellationToken);

            await _documentRepository.UpdateAsync(doc, cancellationToken);
            await _documentRepository.CommitTransactionAsync(cancellationToken);

            return new ReprocessResponse
            {
                Success = true,
                Id = doc.Id,
                Message = "Documento reprocessado com sucesso.",
                ChunkCount = doc.ChunkCount
            };
        }
        catch (Exception ex)
        {
            await _documentRepository.RollbackTransactionAsync(cancellationToken);
            
            doc.Status = KnowledgeDocumentStatus.Error.ToString().ToLowerInvariant();
            doc.ErrorMessage = ex.Message;
            await _documentRepository.UpdateAsync(doc, cancellationToken);
            
            throw new Exception($"Erro ao reprocessar documento: {ex.Message}", ex);
        }
    }
}
