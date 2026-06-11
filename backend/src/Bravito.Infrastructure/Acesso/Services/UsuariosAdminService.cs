using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bravito.Application.Acesso.Interfaces;
using Bravito.Application.Acesso.Models;
using Bravito.Domain.Acesso;
using Bravito.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Bravito.Infrastructure.Acesso.Services
{
    public class UsuariosAdminService : IUsuariosAdminService
    {
        private readonly BravitoDbContext _context;
        private readonly IKeycloakAdminService _keycloakAdminService;

        public UsuariosAdminService(BravitoDbContext context, IKeycloakAdminService keycloakAdminService)
        {
            _context = context;
            _keycloakAdminService = keycloakAdminService;
        }

        public async Task<UsuarioResponse> CriarUsuarioAsync(CriarUsuarioRequest request, CancellationToken cancellationToken = default)
        {
            // 1. Validar e-mail duplicado no banco local
            var existeEmail = await _context.Usuarios.AnyAsync(u => u.Email == request.Email, cancellationToken);
            if (existeEmail)
            {
                throw new InvalidOperationException("Já existe um usuário com este e-mail no sistema.");
            }

            // 2. Validar perfis
            var perfisExistentes = await _context.PerfisAcesso
                .Where(p => request.PerfilIds.Contains(p.Id))
                .Select(p => p.Id)
                .ToListAsync(cancellationToken);

            if (perfisExistentes.Count != request.PerfilIds.Count)
            {
                throw new InvalidOperationException("Um ou mais perfis informados são inválidos.");
            }

            // 3. Criar no Keycloak
            var keycloakId = await _keycloakAdminService.CriarUsuarioAsync(request.Nome, request.Email, request.SenhaTemporaria, request.Ativo, cancellationToken);

            // 4. Criar no Banco Local
            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                KeycloakId = keycloakId,
                Nome = request.Nome,
                Email = request.Email,
                Ativo = request.Ativo,
                DataCriacao = DateTime.UtcNow
            };

            foreach (var perfilId in request.PerfilIds)
            {
                usuario.PerfisAcesso.Add(new UsuarioPerfilAcesso
                {
                    Id = Guid.NewGuid(),
                    UsuarioId = usuario.Id,
                    PerfilAcessoId = perfilId,
                    DataCriacao = DateTime.UtcNow
                });
            }

            await _context.Usuarios.AddAsync(usuario, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return await ObterPorIdAsync(usuario.Id, cancellationToken);
        }

        public async Task<UsuarioResponse> EditarUsuarioAsync(Guid id, EditarUsuarioRequest request, CancellationToken cancellationToken = default)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.PerfisAcesso)
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            // Valida email duplicado
            if (usuario.Email != request.Email)
            {
                var existeEmail = await _context.Usuarios.AnyAsync(u => u.Email == request.Email && u.Id != id, cancellationToken);
                if (existeEmail)
                {
                    throw new InvalidOperationException("Já existe outro usuário com este e-mail no sistema.");
                }
            }

            // Validar perfis
            var perfisExistentes = await _context.PerfisAcesso
                .Where(p => request.PerfilIds.Contains(p.Id))
                .Select(p => p.Id)
                .ToListAsync(cancellationToken);

            if (perfisExistentes.Count != request.PerfilIds.Count)
            {
                throw new InvalidOperationException("Um ou mais perfis informados são inválidos.");
            }

            // Atualiza Keycloak
            await _keycloakAdminService.AtualizarUsuarioAsync(usuario.KeycloakId, request.Nome, request.Email, request.Ativo, cancellationToken);

            // Atualiza Banco diretamente (bypass tracking bugs)
            await _context.Usuarios
                .Where(u => u.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(u => u.Nome, request.Nome)
                    .SetProperty(u => u.Email, request.Email)
                    .SetProperty(u => u.Ativo, request.Ativo)
                    .SetProperty(u => u.DataAlteracao, DateTime.UtcNow), 
                    cancellationToken);

            // Remove todos os perfis atuais diretamente no banco
            await _context.UsuariosPerfisAcesso
                .Where(pa => pa.UsuarioId == id)
                .ExecuteDeleteAsync(cancellationToken);

            // Adiciona os novos perfis selecionados
            foreach (var perfilId in request.PerfilIds)
            {
                _context.UsuariosPerfisAcesso.Add(new UsuarioPerfilAcesso
                {
                    Id = Guid.NewGuid(),
                    UsuarioId = id,
                    PerfilAcessoId = perfilId,
                    DataCriacao = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync(cancellationToken);

            return await ObterPorIdAsync(usuario.Id, cancellationToken);
        }

        public async Task<UsuarioResponse> AlterarStatusUsuarioAsync(Guid id, bool ativo, CancellationToken cancellationToken = default)
        {
            var usuario = await _context.Usuarios.FindAsync(new object[] { id }, cancellationToken);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            if (usuario.Ativo == ativo)
            {
                return await ObterPorIdAsync(id, cancellationToken); // Nenhuma mudança
            }

            // Evitar desativar a si próprio? Poderia validar usando IUsuarioAtualService, mas para simplificar, apenas atualiza.
            
            await _keycloakAdminService.HabilitarDesabilitarUsuarioAsync(usuario.KeycloakId, ativo, cancellationToken);

            usuario.Ativo = ativo;
            usuario.DataAlteracao = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);

            return await ObterPorIdAsync(id, cancellationToken);
        }

        public async Task<UsuarioResponse> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var usuario = await _context.Usuarios
                .Include(u => u.PerfisAcesso)
                .ThenInclude(pa => pa.PerfilAcesso)
                .ThenInclude(p => p.Recursos)
                .ThenInclude(pr => pr.Recurso)
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            var perfis = usuario.PerfisAcesso.Select(pa => pa.PerfilAcesso.Nome).ToList();
            var recursos = usuario.PerfisAcesso
                .SelectMany(pa => pa.PerfilAcesso.Recursos)
                .Where(pr => pr.Recurso.Ativo)
                .Select(pr => pr.Recurso.Codigo)
                .Distinct()
                .ToList();

            return new UsuarioResponse
            {
                Id = usuario.Id,
                KeycloakId = usuario.KeycloakId,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Ativo = usuario.Ativo,
                DataCriacao = usuario.DataCriacao,
                DataAlteracao = usuario.DataAlteracao,
                Perfis = perfis,
                Recursos = recursos
            };
        }

        public async Task<List<UsuarioResponse>> ListarUsuariosAsync(CancellationToken cancellationToken = default)
        {
            var usuarios = await _context.Usuarios
                .Include(u => u.PerfisAcesso)
                .ThenInclude(pa => pa.PerfilAcesso)
                .OrderBy(u => u.Nome)
                .ToListAsync(cancellationToken);

            var list = new List<UsuarioResponse>();

            foreach (var u in usuarios)
            {
                list.Add(new UsuarioResponse
                {
                    Id = u.Id,
                    KeycloakId = u.KeycloakId,
                    Nome = u.Nome,
                    Email = u.Email,
                    Ativo = u.Ativo,
                    DataCriacao = u.DataCriacao,
                    DataAlteracao = u.DataAlteracao,
                    Perfis = u.PerfisAcesso.Select(pa => pa.PerfilAcesso.Nome).ToList(),
                    Recursos = new List<string>() // Pode ser omitido na listagem para performance
                });
            }

            return list;
        }
    }
}
