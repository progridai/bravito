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
    [AllowAnonymous] // Permitindo acesso livre temporário para testes
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

            // BYPASS: Preenchendo dados falsos do usuário para não quebrar a integração com o n8n
            var usuario = new UsuarioAutenticado
            {
                Id = "teste123",
                NomeUsuario = "usuario_teste",
                Email = "teste@bravito.local",
                EmpresaId = "empresa_teste"
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
