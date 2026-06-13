using System;
using System.Threading;
using System.Threading.Tasks;
using Bravito.Application.Knowledge.DTOs;
using Bravito.Application.Knowledge.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bravito.Api.Controllers;

[ApiController]
[Route("api/knowledge")]
[Authorize] // Segue o padrão atual de autenticação
public class KnowledgeDocumentsController : ControllerBase
{
    private readonly IKnowledgeDocumentService _knowledgeService;
    private readonly IKnowledgeDocumentRepository _repository;

    public KnowledgeDocumentsController(IKnowledgeDocumentService knowledgeService, IKnowledgeDocumentRepository repository)
    {
        _knowledgeService = knowledgeService;
        _repository = repository;
    }

    [HttpGet("health")]
    [AllowAnonymous]
    public async Task<IActionResult> HealthCheck(CancellationToken cancellationToken)
    {
        var result = await _knowledgeService.CheckHealthAsync(cancellationToken);
        if (!result.Success)
            return StatusCode(500, result);

        return Ok(result);
    }

    [HttpGet("documents")]
    public async Task<IActionResult> ListDocuments([FromQuery] string? app, [FromQuery] bool includeDeleted = false, CancellationToken cancellationToken = default)
    {
        var docs = await _repository.ListAsync(app, includeDeleted, cancellationToken);
        return Ok(docs);
    }

    [HttpGet("documents/{id}")]
    public async Task<IActionResult> GetDocument(Guid id, CancellationToken cancellationToken)
    {
        var doc = await _repository.GetByIdAsync(id, cancellationToken);
        if (doc == null || doc.DeletedAt != null)
            return NotFound();

        return Ok(doc);
    }

    [HttpPost("documents/upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadDocument([FromForm] IFormFile file, [FromForm] string? app, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
            return BadRequest(new ErrorResponse { Message = "Nenhum arquivo enviado." });

        try
        {
            using var stream = file.OpenReadStream();
            var response = await _knowledgeService.UploadAsync(stream, file.FileName, app, cancellationToken);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse
            {
                Message = "Erro ao processar documento.",
                Details = ex.Message,
                Step = "upload_process"
            });
        }
    }

    [HttpDelete("documents/{id}")]
    public async Task<IActionResult> DeleteDocument(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _knowledgeService.DeleteAsync(id, cancellationToken);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse { Message = "Erro ao deletar documento.", Details = ex.Message });
        }
    }

    [HttpPost("documents/{id}/replace")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> ReplaceDocument(Guid id, [FromForm] IFormFile file, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
            return BadRequest(new ErrorResponse { Message = "Nenhum arquivo enviado." });

        try
        {
            using var stream = file.OpenReadStream();
            var response = await _knowledgeService.ReplaceAsync(id, stream, file.FileName, cancellationToken);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse { Message = "Erro ao substituir documento.", Details = ex.Message });
        }
    }

    [HttpPost("documents/{id}/reprocess")]
    public async Task<IActionResult> ReprocessDocument(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _knowledgeService.ReprocessAsync(id, cancellationToken);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse { Message = "Erro ao reprocessar documento.", Details = ex.Message });
        }
    }
}
