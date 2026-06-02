using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using Bravito.Application.Chat.Interfaces;
using Bravito.Application.Chat.Models;

namespace Bravito.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IAssistenteChatService _assistenteChatService;

        public ChatController(IAssistenteChatService assistenteChatService)
        {
            _assistenteChatService = assistenteChatService;
        }

        [HttpPost("enviar")]
        public async Task<IActionResult> EnviarMensagem([FromBody] EnviarMensagemChatRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Mensagem))
            {
                return BadRequest(new { erro = "A mensagem não pode estar vazia." });
            }

            var usuario = new UsuarioAutenticado
            {
                Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty,
                NomeUsuario = User.FindFirst("preferred_username")?.Value ?? string.Empty,
                Email = User.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty,
                EmpresaId = null // TODO: Obter de claim ou banco no futuro
            };

            var resposta = await _assistenteChatService.EnviarMensagemAsync(request, usuario, cancellationToken);

            if (!resposta.Sucesso && !string.IsNullOrEmpty(resposta.MensagemErro))
            {
                return StatusCode(502, new { erro = resposta.MensagemErro });
            }

            return Ok(resposta);
        }
    }
}
