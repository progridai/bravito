using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bravito.Application.Acesso.Interfaces;
using Bravito.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bravito.Infrastructure.Acesso.Services
{
    public class AutorizacaoAplicacaoService : IAutorizacaoAplicacaoService
    {
        private readonly BravitoDbContext _context;

        public AutorizacaoAplicacaoService(BravitoDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UsuarioPossuiRecursoAsync(Guid usuarioId, string codigoRecurso)
        {
            var possui = await _context.UsuariosPerfisAcesso
                .Where(upa => upa.UsuarioId == usuarioId)
                .Join(_context.PerfisAcessoRecursos,
                      upa => upa.PerfilAcessoId,
                      par => par.PerfilAcessoId,
                      (upa, par) => par.Recurso)
                .AnyAsync(r => r.Codigo == codigoRecurso && r.Ativo);

            return possui;
        }

        public async Task<IReadOnlyCollection<string>> ObterRecursosUsuarioAsync(Guid usuarioId)
        {
            var recursos = await _context.UsuariosPerfisAcesso
                .Where(upa => upa.UsuarioId == usuarioId)
                .Join(_context.PerfisAcessoRecursos,
                      upa => upa.PerfilAcessoId,
                      par => par.PerfilAcessoId,
                      (upa, par) => par.Recurso)
                .Where(r => r.Ativo)
                .Select(r => r.Codigo)
                .Distinct()
                .ToListAsync();

            return recursos;
        }
    }
}
