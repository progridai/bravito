using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bravito.Application.Acesso.Models;

namespace Bravito.Application.Acesso.Interfaces
{
    public interface IUsuariosAdminService
    {
        Task<UsuarioResponse> CriarUsuarioAsync(CriarUsuarioRequest request, CancellationToken cancellationToken = default);
        Task<UsuarioResponse> EditarUsuarioAsync(Guid id, EditarUsuarioRequest request, CancellationToken cancellationToken = default);
        Task<UsuarioResponse> AlterarStatusUsuarioAsync(Guid id, bool ativo, CancellationToken cancellationToken = default);
        Task<UsuarioResponse> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<List<UsuarioResponse>> ListarUsuariosAsync(CancellationToken cancellationToken = default);
    }
}
