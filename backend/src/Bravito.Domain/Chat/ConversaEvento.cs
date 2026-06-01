using System;

namespace Bravito.Domain.Chat
{
    public class ConversaEvento
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ConversaId { get; set; }
        public string TipoEvento { get; set; } = string.Empty;
        public string? Detalhes { get; set; } // JSONB
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public Conversa Conversa { get; set; } = null!;
    }
}
