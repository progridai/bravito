using System;
using System.Threading;
using System.Threading.Tasks;
using Bravito.Application.Acesso.Interfaces;
using Bravito.Domain.Acesso;
using Bravito.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bravito.Infrastructure.Acesso.Services
{
    public class UsuarioAplicacaoService : IUsuarioAplicacaoService
    {
        private readonly BravitoDbContext _context;
        private readonly IUsuarioAtualService _usuarioAtualService;

        public UsuarioAplicacaoService(BravitoDbContext context, IUsuarioAtualService usuarioAtualService)
        {
            _context = context;
            _usuarioAtualService = usuarioAtualService;
        }

        public async Task<Usuario> SincronizarUsuarioAtualAsync(CancellationToken cancellationToken = default)
        {
            var keycloakId = _usuarioAtualService.ObterKeycloakId();
            if (string.IsNullOrEmpty(keycloakId))
                throw new InvalidOperationException("Usuário não autenticado ou KeycloakId não encontrado.");

            var email = _usuarioAtualService.ObterEmail() ?? string.Empty;
            var nome = _usuarioAtualService.ObterNome() ?? "Usuário";

            var usuario = await _context.Usuarios
                .Include(u => u.PerfisAcesso)
                    .ThenInclude(pa => pa.PerfilAcesso)
                .FirstOrDefaultAsync(u => u.KeycloakId == keycloakId, cancellationToken);

            if (usuario == null)
            {
                usuario = new Usuario
                {
                    Id = Guid.NewGuid(),
                    KeycloakId = keycloakId,
                    Nome = nome,
                    Email = email,
                    Ativo = true,
                    DataCriacao = DateTime.UtcNow
                };
                // Se for o primeiro usuário (ou não há nenhum), será tratado no fluxo abaixo
                await _context.Usuarios.AddAsync(usuario, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                bool modified = false;
                if (usuario.Nome != nome)
                {
                    usuario.Nome = nome;
                    modified = true;
                }
                if (usuario.Email != email)
                {
                    usuario.Email = email;
                    modified = true;
                }

                if (modified)
                {
                    usuario.DataAlteracao = DateTime.UtcNow;
                    _context.Usuarios.Update(usuario);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }

            // Garante que o primeiro usuário a logar (ou se o sistema estiver sem admins) vire Administrador
            var adminPerfilId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var existeAdmin = await _context.UsuariosPerfisAcesso.AnyAsync(upa => upa.PerfilAcessoId == adminPerfilId, cancellationToken);
            
            if (!existeAdmin && !usuario.PerfisAcesso.Any(pa => pa.PerfilAcessoId == adminPerfilId))
            {
                var novoVinculo = new UsuarioPerfilAcesso
                {
                    Id = Guid.NewGuid(),
                    PerfilAcessoId = adminPerfilId,
                    UsuarioId = usuario.Id,
                    DataCriacao = DateTime.UtcNow
                };
                usuario.PerfisAcesso.Add(novoVinculo);
                
                if (_context.Entry(usuario).State == EntityState.Detached)
                {
                    _context.UsuariosPerfisAcesso.Add(novoVinculo);
                }
                
                await _context.SaveChangesAsync(cancellationToken);
            }
            return usuario;
        }
    }
}
