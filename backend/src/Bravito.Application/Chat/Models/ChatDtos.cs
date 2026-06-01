using System;
using System.Collections.Generic;

namespace Bravito.Application.Chat.Models
{
    public class ConversaDto
    {
        public Guid Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public string CanalOrigem { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime? DataUltimaInteracao { get; set; }
    }

    public class CriarConversaRequest
    {
        public string? IdentificadorExterno { get; set; }
        public string CanalOrigem { get; set; } = "api";
    }

    public class CriarMensagemRequest
    {
        public string Mensagem { get; set; } = string.Empty;
    }
}
