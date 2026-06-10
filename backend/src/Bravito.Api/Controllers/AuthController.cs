using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using Bravito.Application.Acesso.Interfaces;

namespace Bravito.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioAplicacaoService _usuarioAplicacaoService;
        private readonly IAutorizacaoAplicacaoService _autorizacaoAplicacaoService;

        public AuthController(IUsuarioAplicacaoService usuarioAplicacaoService, IAutorizacaoAplicacaoService autorizacaoAplicacaoService)
        {
            _usuarioAplicacaoService = usuarioAplicacaoService;
            _autorizacaoAplicacaoService = autorizacaoAplicacaoService;
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            // Sincroniza o usuário atual com o banco (cria se primeiro login)
            var usuario = await _usuarioAplicacaoService.SincronizarUsuarioAtualAsync();

            // Busca os recursos
            var recursos = await _autorizacaoAplicacaoService.ObterRecursosUsuarioAsync(usuario.Id);

            var userInfo = new
            {
                UsuarioId = usuario.Id.ToString(),
                KeycloakId = usuario.KeycloakId,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Perfis = usuario.PerfisAcesso.Select(p => p.PerfilAcesso.Nome).ToList(),
                Recursos = recursos
            };

            return Ok(userInfo);
        }
    }
}
