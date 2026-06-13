using System;
using System.Collections.Generic;

namespace Bravito.Application.Acesso.Models
{
    public class UsuarioResponse
    {
        public Guid Id { get; set; }
        public string KeycloakId { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public List<string> Perfis { get; set; } = new List<string>();
        public List<string> Recursos { get; set; } = new List<string>();
    }
}
