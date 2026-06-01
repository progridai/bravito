using System;

namespace Bravito.Domain.Chat
{
    public class ConversaMensagem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ConversaId { get; set; }
        public string TipoRemetente { get; set; } = string.Empty; // usuario, assistente, sistema, ferramenta
        public string Conteudo { get; set; } = string.Empty;
        public string? ConteudoBruto { get; set; } // JSONB
        public int? TokensEntrada { get; set; }
        public int? TokensSaida { get; set; }
        public string? ModeloUsado { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "enviada";

        public Conversa Conversa { get; set; } = null!;
    }
}
