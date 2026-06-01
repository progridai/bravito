namespace Bravito.Application.Chat.Models
{
    public class EnviarMensagemChatRequest
    {
        public string? ConversaId { get; set; }
        public string Mensagem { get; set; } = string.Empty;
    }
}
