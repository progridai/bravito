using System;
using Bravito.Domain.Knowledge.Enums;

namespace Bravito.Domain.Knowledge.Entities;

public class KnowledgeDocument
{
    public Guid Id { get; set; }
    public string App { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string FileHash { get; set; } = string.Empty;
    public string MimeType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string Status { get; set; } = KnowledgeDocumentStatus.Uploaded.ToString().ToLowerInvariant();
    public int ChunkCount { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ProcessedAt { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime? DeletedAt { get; set; }
}
