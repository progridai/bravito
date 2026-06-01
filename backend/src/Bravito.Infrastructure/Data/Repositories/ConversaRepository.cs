using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bravito.Application.Chat.Interfaces;
using Bravito.Domain.Chat;
using Microsoft.EntityFrameworkCore;

namespace Bravito.Infrastructure.Data.Repositories
{
    public class ConversaRepository : IConversaRepository
    {
        private readonly DbContext _context; // Assumindo BravitoDbContext no futuro

        public ConversaRepository(DbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Conversa conversa, CancellationToken cancellationToken)
        {
            await _context.Set<Conversa>().AddAsync(conversa, cancellationToken);
        }

        public async Task AdicionarEventoAsync(ConversaEvento evento, CancellationToken cancellationToken)
        {
            await _context.Set<ConversaEvento>().AddAsync(evento, cancellationToken);
        }

        public async Task AdicionarMensagemAsync(ConversaMensagem mensagem, CancellationToken cancellationToken)
        {
            await _context.Set<ConversaMensagem>().AddAsync(mensagem, cancellationToken);
        }

        public Task AtualizarAsync(Conversa conversa, CancellationToken cancellationToken)
        {
            _context.Set<Conversa>().Update(conversa);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<ConversaMensagem>> ObterMensagensPorConversaIdAsync(Guid conversaId, CancellationToken cancellationToken)
        {
            return await _context.Set<ConversaMensagem>()
                .Where(m => m.ConversaId == conversaId)
                .OrderBy(m => m.DataCriacao)
                .ToListAsync(cancellationToken);
        }

        public async Task<Conversa?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<Conversa>()
                .Include(c => c.Mensagens.OrderByDescending(m => m.DataCriacao).Take(50))
                .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Conversa>> ObterPorUsuarioIdAsync(string usuarioId, CancellationToken cancellationToken)
        {
            return await _context.Set<Conversa>()
                .Where(c => c.UsuarioId == usuarioId)
                .OrderByDescending(c => c.DataUltimaInteracao ?? c.DataCriacao)
                .ToListAsync(cancellationToken);
        }

        public async Task SalvarAlteracoesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
