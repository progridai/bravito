namespace Bravito.Infrastructure.Integrations.N8n.Options
{
    public class N8nOptions
    {
        public string WebhookUrl { get; set; } = string.Empty;
        public int TimeoutSeconds { get; set; } = 60;
    }
}
