namespace Bravito.Infrastructure.Knowledge.Options;

public class KnowledgeOptions
{
    public string DbSchema { get; set; } = "public";
    public string VectorTable { get; set; } = "documents";
    public string StoragePath { get; set; } = "/app/storage/documents";
    public string AllowedExtensions { get; set; } = ".pdf,.docx,.txt";
    public int MaxFileSizeMb { get; set; } = 25;
    public string DefaultApp { get; set; } = "bravito";
}
