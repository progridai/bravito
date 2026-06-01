using System;

namespace Bravito.Domain.Chat
{
    public class ConversaContexto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ConversaId { get; set; }
        public string ResumoAtual { get; set; } = string.Empty;
        public string? DadosAuxiliares { get; set; } // JSONB
        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;

        public Conversa Conversa { get; set; } = null!;
    }
}
