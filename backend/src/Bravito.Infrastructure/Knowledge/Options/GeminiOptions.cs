namespace Bravito.Infrastructure.Knowledge.Options;

public class GeminiOptions
{
    public string ApiKey { get; set; } = string.Empty;
    public string EmbeddingModel { get; set; } = "models/text-embedding-004";
}
