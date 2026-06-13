namespace Bravito.Infrastructure.Knowledge.Options;

public class GeminiOptions
{
    public string ApiKey { get; set; } = string.Empty;
    public string EmbeddingModel { get; set; } = "models/embedding-001";
}
