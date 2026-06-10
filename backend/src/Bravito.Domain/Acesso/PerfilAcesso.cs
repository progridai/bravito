using System;
using System.Collections.Generic;

namespace Bravito.Domain.Acesso
{
    public class PerfilAcesso
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime? DataAlteracao { get; set; }

        public ICollection<UsuarioPerfilAcesso> Usuarios { get; set; } = new List<UsuarioPerfilAcesso>();
        public ICollection<PerfilAcessoRecurso> Recursos { get; set; } = new List<PerfilAcessoRecurso>();
    }
}
