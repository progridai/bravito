using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Threading;
using Bravito.Application.Chat.Interfaces;
using Bravito.Application.Chat.Models;
using Bravito.Domain.Chat;

namespace Bravito.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IAssistenteChatService _assistenteChatService;
        private readonly IConversaRepository _conversaRepository;

        public ChatController(IAssistenteChatService assistenteChatService, IConversaRepository conversaRepository)
        {
            _assistenteChatService = assistenteChatService;
            _conversaRepository = conversaRepository;
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

            // 1. Resolve a Conversa no Banco de Dados
            Conversa? conversa = null;
            if (!string.IsNullOrEmpty(request.ConversaId) && Guid.TryParse(request.ConversaId, out var id))
            {
                conversa = await _conversaRepository.ObterPorIdAsync(id, cancellationToken);
            }

            if (conversa == null)
            {
                conversa = new Conversa { UsuarioId = usuario.Id };
                if (!string.IsNullOrEmpty(request.ConversaId) && Guid.TryParse(request.ConversaId, out var parsedId))
                {
                    conversa.Id = parsedId;
                }
                await _conversaRepository.AdicionarAsync(conversa, cancellationToken);
            }

            // Garante que o n8n vai receber o ID oficial da conversa gerado pelo banco
            request.ConversaId = conversa.Id.ToString();

            // 2. Salva a mensagem do Usuário
            var msgUsuario = new ConversaMensagem
            {
                ConversaId = conversa.Id,
                TipoRemetente = "usuario",
                Conteudo = request.Mensagem
            };
            await _conversaRepository.AdicionarMensagemAsync(msgUsuario, cancellationToken);
            
            conversa.DataUltimaInteracao = DateTime.UtcNow;
            await _conversaRepository.SalvarAlteracoesAsync(cancellationToken);

            // 3. Comunica com o assistente n8n
            var resposta = await _assistenteChatService.EnviarMensagemAsync(request, usuario, cancellationToken);

            if (!resposta.Sucesso && !string.IsNullOrEmpty(resposta.MensagemErro))
            {
                return StatusCode(502, new { erro = resposta.MensagemErro });
            }

            // 4. Salva a resposta do Assistente (n8n)
            var msgAssistente = new ConversaMensagem
            {
                ConversaId = conversa.Id,
                TipoRemetente = "assistente",
                Conteudo = resposta.Resposta
            };
            await _conversaRepository.AdicionarMensagemAsync(msgAssistente, cancellationToken);
            
            conversa.DataUltimaInteracao = DateTime.UtcNow;
            await _conversaRepository.SalvarAlteracoesAsync(cancellationToken);

            return Ok(resposta);
        }
    }
}
