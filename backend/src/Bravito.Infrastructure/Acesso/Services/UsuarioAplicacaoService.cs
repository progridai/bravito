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

                // Se for o primeiro usuário, atribui perfil Administrador
                var isFirstUser = !await _context.Usuarios.AnyAsync(cancellationToken);
                if (isFirstUser)
                {
                    var adminPerfilId = Guid.Parse("11111111-1111-1111-1111-111111111111");
                    usuario.PerfisAcesso.Add(new UsuarioPerfilAcesso
                    {
                        Id = Guid.NewGuid(),
                        PerfilAcessoId = adminPerfilId,
                        UsuarioId = usuario.Id,
                        DataCriacao = DateTime.UtcNow
                    });
                }

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

            return usuario;
        }
    }
}
