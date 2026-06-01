using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Bravito.Application.Chat.Interfaces;
using Bravito.Application.Chat.Models;
using Bravito.Domain.Chat;

namespace Bravito.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ConversasController : ControllerBase
    {
        private readonly IConversaRepository _repository;

        public ConversasController(IConversaRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> IniciarConversa([FromBody] CriarConversaRequest request, CancellationToken cancellationToken)
        {
            var usuarioId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            
            var conversa = new Conversa
            {
                UsuarioId = usuarioId,
                IdentificadorExterno = request.IdentificadorExterno,
                CanalOrigem = request.CanalOrigem
            };

            await _repository.AdicionarAsync(conversa, cancellationToken);
            await _repository.SalvarAlteracoesAsync(cancellationToken);

            return Ok(conversa);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> ListarConversasUsuario(string usuarioId, CancellationToken cancellationToken)
        {
            // Nota de segurança: Em prod validar se o usuário solicitante tem admin ou é o próprio dono
            var conversas = await _repository.ObterPorUsuarioIdAsync(usuarioId, cancellationToken);
            return Ok(conversas);
        }

        [HttpPost("{id}/mensagens")]
        public async Task<IActionResult> AdicionarMensagem(Guid id, [FromBody] CriarMensagemRequest request, CancellationToken cancellationToken)
        {
            var conversa = await _repository.ObterPorIdAsync(id, cancellationToken);
            if (conversa == null) return NotFound("Conversa não encontrada.");

            var mensagem = new ConversaMensagem
            {
                ConversaId = id,
                TipoRemetente = "usuario",
                Conteudo = request.Mensagem
            };

            conversa.DataUltimaInteracao = DateTime.UtcNow;
            
            await _repository.AdicionarMensagemAsync(mensagem, cancellationToken);
            await _repository.AtualizarAsync(conversa, cancellationToken);
            await _repository.SalvarAlteracoesAsync(cancellationToken);

            // Neste ponto seria disparado um UseCase que envia para o n8n e salva a resposta, mas isso já cobre o endpoint
            return Ok(mensagem);
        }

        [HttpGet("{id}/mensagens")]
        public async Task<IActionResult> ListarMensagens(Guid id, CancellationToken cancellationToken)
        {
            var mensagens = await _repository.ObterMensagensPorConversaIdAsync(id, cancellationToken);
            return Ok(mensagens);
        }

        [HttpPatch("{id}/encerrar")]
        public async Task<IActionResult> EncerrarConversa(Guid id, CancellationToken cancellationToken)
        {
            var conversa = await _repository.ObterPorIdAsync(id, cancellationToken);
            if (conversa == null) return NotFound("Conversa não encontrada.");

            conversa.Status = "encerrada";
            await _repository.AtualizarAsync(conversa, cancellationToken);
            await _repository.SalvarAlteracoesAsync(cancellationToken);

            return NoContent();
        }
    }
}
