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
        private readonly IKeycloakAdminService _keycloakAdminService;

        public AuthController(
            IUsuarioAplicacaoService usuarioAplicacaoService, 
            IAutorizacaoAplicacaoService autorizacaoAplicacaoService,
            IKeycloakAdminService keycloakAdminService)
        {
            _usuarioAplicacaoService = usuarioAplicacaoService;
            _autorizacaoAplicacaoService = autorizacaoAplicacaoService;
            _keycloakAdminService = keycloakAdminService;
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
                Perfis = usuario.PerfisAcesso.Where(p => p.PerfilAcesso != null).Select(p => p.PerfilAcesso.Nome).ToList(),
                Recursos = recursos
            };

            return Ok(userInfo);
        }

        [HttpPost("alterar-senha")]
        [Authorize]
        public async Task<IActionResult> AlterarSenha([FromBody] Bravito.Application.Acesso.Models.AlterarSenhaRequest request, System.Threading.CancellationToken cancellationToken)
        {
            var keycloakId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(keycloakId))
            {
                return Unauthorized();
            }

            try
            {
                await _keycloakAdminService.AlterarSenhaAsync(keycloakId, request.NovaSenha, false, cancellationToken);
                return Ok(new { sucesso = true });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao alterar senha. " + ex.Message });
            }
        }
    }
}
