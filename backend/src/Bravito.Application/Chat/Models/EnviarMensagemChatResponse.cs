namespace Bravito.Application.Chat.Models
{
    public class EnviarMensagemChatResponse
    {
        public bool Sucesso { get; set; }
        public string? ConversaId { get; set; }
        public string Resposta { get; set; } = string.Empty;
        public string? MensagemErro { get; set; }
        public object? Metadados { get; set; }
    }
}
