using System.Threading;
using System.Threading.Tasks;
using Bravito.Domain.Acesso;

namespace Bravito.Application.Acesso.Interfaces
{
    public interface IUsuarioAplicacaoService
    {
        Task<Usuario> SincronizarUsuarioAtualAsync(CancellationToken cancellationToken = default);
    }
}
