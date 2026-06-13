namespace Bravito.Application.Knowledge.DTOs;

public class HealthResponse
{
    public bool Success { get; set; }
    public string Database { get; set; } = string.Empty;
    public string DocumentsTable { get; set; } = string.Empty;
    public string KnowledgeDocumentsTable { get; set; } = string.Empty;
    public string StoragePath { get; set; } = string.Empty;
    public string StoragePathValue { get; set; } = string.Empty;
    public string GeminiApiKey { get; set; } = string.Empty;
    public string EmbeddingModel { get; set; } = string.Empty;
    public string? ErrorMessage { get; set; }
}
