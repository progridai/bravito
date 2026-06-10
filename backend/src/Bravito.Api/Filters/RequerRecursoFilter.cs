using System.Threading.Tasks;
using Bravito.Application.Acesso.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Bravito.Api.Filters
{
    public class RequerRecursoFilter : IAsyncAuthorizationFilter
    {
        private readonly string _recurso;
        private readonly IUsuarioAplicacaoService _usuarioAplicacaoService;
        private readonly IAutorizacaoAplicacaoService _autorizacaoAplicacaoService;

        public RequerRecursoFilter(string recurso, IUsuarioAplicacaoService usuarioAplicacaoService, IAutorizacaoAplicacaoService autorizacaoAplicacaoService)
        {
            _recurso = recurso;
            _usuarioAplicacaoService = usuarioAplicacaoService;
            _autorizacaoAplicacaoService = autorizacaoAplicacaoService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            // O usuário já deve estar autenticado pelo JWT Bearer (AuthorizeAttribute global ou no controller)
            if (context.HttpContext.User.Identity?.IsAuthenticated != true)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Sincroniza o usuário atual no banco (cria se não existir, atualiza dados)
            var usuario = await _usuarioAplicacaoService.SincronizarUsuarioAtualAsync();

            // Valida se o usuário possui o recurso
            var possuiRecurso = await _autorizacaoAplicacaoService.UsuarioPossuiRecursoAsync(usuario.Id, _recurso);

            if (!possuiRecurso)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
