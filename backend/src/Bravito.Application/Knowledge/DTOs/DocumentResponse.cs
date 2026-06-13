using System;

namespace Bravito.Application.Knowledge.DTOs;

public class DocumentResponse
{
    public Guid Id { get; set; }
    public string App { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string? FilePath { get; set; }
    public string? FileHash { get; set; }
    public string MimeType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string Status { get; set; } = string.Empty;
    public int ChunkCount { get; set; }
    public DateTime UploadedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public string? ErrorMessage { get; set; }
}
