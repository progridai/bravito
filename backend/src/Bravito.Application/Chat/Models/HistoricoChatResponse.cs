using System;
using System.Collections.Generic;

namespace Bravito.Application.Chat.Models
{
    public class HistoricoChatResponse
    {
        public bool Sucesso { get; set; }
        public string? ConversaId { get; set; }
        public List<MensagemChatDto> Mensagens { get; set; } = new List<MensagemChatDto>();
    }

    public class MensagemChatDto
    {
        public string Id { get; set; } = string.Empty;
        public string TipoRemetente { get; set; } = string.Empty;
        public string Conteudo { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
    }
}
