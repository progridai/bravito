using System.Threading;
using System.Threading.Tasks;

namespace Bravito.Application.Acesso.Interfaces
{
    public interface IKeycloakAdminService
    {
        Task<string> CriarUsuarioAsync(string username, string nome, string email, string senhaTemporaria, bool ativo, CancellationToken cancellationToken = default);
        Task AtualizarUsuarioAsync(string keycloakId, string username, string nome, string email, bool ativo, CancellationToken cancellationToken = default);
        Task HabilitarDesabilitarUsuarioAsync(string keycloakId, bool ativo, CancellationToken cancellationToken = default);
        Task AlterarSenhaAsync(string keycloakId, string novaSenha, bool temporaria = false, CancellationToken cancellationToken = default);
    }
}
