using System;
using System.Collections.Generic;

namespace Bravito.Application.Acesso.Models
{
    public class PerfilAcessoResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public bool Ativo { get; set; }
        public List<string> Recursos { get; set; } = new List<string>();
    }
}
