using System;
using System.Collections.Generic;

namespace Bravito.Domain.Acesso
{
    public class Usuario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string KeycloakId { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime? DataAlteracao { get; set; }

        public ICollection<UsuarioPerfilAcesso> PerfisAcesso { get; set; } = new List<UsuarioPerfilAcesso>();
    }
}
