using System.Collections.Generic;

namespace Bravito.Application.Acesso.Interfaces
{
    public interface IUsuarioAtualService
    {
        string? ObterKeycloakId();
        string? ObterEmail();
        string? ObterNome();
        IReadOnlyCollection<string> ObterRoles();
    }
}
