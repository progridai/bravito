using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bravito.Application.Acesso.Interfaces
{
    public interface IAutorizacaoAplicacaoService
    {
        Task<bool> UsuarioPossuiRecursoAsync(Guid usuarioId, string codigoRecurso);
        Task<IReadOnlyCollection<string>> ObterRecursosUsuarioAsync(Guid usuarioId);
    }
}
