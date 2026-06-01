using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bravito.Domain.Chat;

namespace Bravito.Application.Chat.Interfaces
{
    public interface IConversaRepository
    {
        Task<Conversa?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Conversa>> ObterPorUsuarioIdAsync(string usuarioId, CancellationToken cancellationToken);
        Task AdicionarAsync(Conversa conversa, CancellationToken cancellationToken);
        Task AtualizarAsync(Conversa conversa, CancellationToken cancellationToken);
        Task AdicionarMensagemAsync(ConversaMensagem mensagem, CancellationToken cancellationToken);
        Task AdicionarEventoAsync(ConversaEvento evento, CancellationToken cancellationToken);
        Task<IEnumerable<ConversaMensagem>> ObterMensagensPorConversaIdAsync(Guid conversaId, CancellationToken cancellationToken);
        Task SalvarAlteracoesAsync(CancellationToken cancellationToken);
    }
}
