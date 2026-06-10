using System;

namespace Bravito.Domain.Acesso
{
    public class PerfilAcessoRecurso
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PerfilAcessoId { get; set; }
        public Guid RecursoId { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

        public PerfilAcesso PerfilAcesso { get; set; } = null!;
        public Recurso Recurso { get; set; } = null!;
    }
}
