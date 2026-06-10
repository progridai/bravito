using System;

namespace Bravito.Domain.Acesso
{
    public class UsuarioPerfilAcesso
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UsuarioId { get; set; }
        public Guid PerfilAcessoId { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public Usuario Usuario { get; set; } = null!;
        public PerfilAcesso PerfilAcesso { get; set; } = null!;
    }
}
