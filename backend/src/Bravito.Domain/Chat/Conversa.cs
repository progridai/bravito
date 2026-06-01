using System;
using System.Collections.Generic;

namespace Bravito.Domain.Chat
{
    public class Conversa
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UsuarioId { get; set; } = string.Empty;
        public string? IdentificadorExterno { get; set; }
        public string CanalOrigem { get; set; } = "api";
        public string Status { get; set; } = "aberta";
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime? DataUltimaInteracao { get; set; }
        public string? Metadados { get; set; } // JSONB

        public ICollection<ConversaMensagem> Mensagens { get; set; } = new List<ConversaMensagem>();
        public ICollection<ConversaEvento> Eventos { get; set; } = new List<ConversaEvento>();
        public ConversaContexto? Contexto { get; set; }
    }
}
