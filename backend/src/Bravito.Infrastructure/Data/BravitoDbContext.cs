using Bravito.Domain.Chat;
using Bravito.Domain.Acesso;
using Microsoft.EntityFrameworkCore;
using Bravito.Infrastructure.Data.Configurations;
using System;
using System.Collections.Generic;

namespace Bravito.Infrastructure.Data
{
    public class BravitoDbContext : DbContext
    {
        public BravitoDbContext(DbContextOptions<BravitoDbContext> options) : base(options)
        {
        }

        public DbSet<Conversa> Conversas { get; set; }
        public DbSet<ConversaMensagem> ConversasMensagens { get; set; }
        public DbSet<ConversaContexto> ConversasContextos { get; set; }
        public DbSet<ConversaEvento> ConversasEventos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<PerfilAcesso> PerfisAcesso { get; set; }
        public DbSet<Recurso> Recursos { get; set; }
        public DbSet<UsuarioPerfilAcesso> UsuariosPerfisAcesso { get; set; }
        public DbSet<PerfilAcessoRecurso> PerfisAcessoRecursos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Aplica as configurações do Fluent API
            modelBuilder.ApplyConfiguration(new ConversaConfiguration());
            modelBuilder.ApplyConfiguration(new ConversaMensagemConfiguration());
            modelBuilder.ApplyConfiguration(new ConversaContextoConfiguration());
            modelBuilder.ApplyConfiguration(new ConversaEventoConfiguration());

            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new PerfilAcessoConfiguration());
            modelBuilder.ApplyConfiguration(new RecursoConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioPerfilAcessoConfiguration());
            modelBuilder.ApplyConfiguration(new PerfilAcessoRecursoConfiguration());

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var adminPerfilId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var operadorPerfilId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var somenteChatPerfilId = Guid.Parse("33333333-3333-3333-3333-333333333333");

            modelBuilder.Entity<PerfilAcesso>().HasData(
                new PerfilAcesso { Id = adminPerfilId, Nome = "Administrador", Descricao = "Acesso total ao sistema", Ativo = true, DataCriacao = DateTime.UtcNow },
                new PerfilAcesso { Id = operadorPerfilId, Nome = "Operador", Descricao = "Acesso a operações diárias e chat", Ativo = true, DataCriacao = DateTime.UtcNow },
                new PerfilAcesso { Id = somenteChatPerfilId, Nome = "Somente Chat", Descricao = "Acesso restrito ao chat", Ativo = true, DataCriacao = DateTime.UtcNow }
            );

            var recursos = new List<Recurso>
            {
                new Recurso { Id = Guid.Parse("a0000000-0000-0000-0000-000000000001"), Codigo = "chat.acessar", Nome = "Acessar Chat", Descricao = "Permite acessar e enviar mensagens no chat", Ativo = true, DataCriacao = DateTime.UtcNow },
                new Recurso { Id = Guid.Parse("a0000000-0000-0000-0000-000000000002"), Codigo = "conversas.visualizar", Nome = "Visualizar Conversas", Descricao = "Permite visualizar histórico de conversas", Ativo = true, DataCriacao = DateTime.UtcNow },
                new Recurso { Id = Guid.Parse("a0000000-0000-0000-0000-000000000003"), Codigo = "usuarios.visualizar", Nome = "Visualizar Usuários", Descricao = "Permite visualizar lista de usuários", Ativo = true, DataCriacao = DateTime.UtcNow },
                new Recurso { Id = Guid.Parse("a0000000-0000-0000-0000-000000000004"), Codigo = "usuarios.cadastrar", Nome = "Cadastrar Usuários", Descricao = "Permite cadastrar novos usuários", Ativo = true, DataCriacao = DateTime.UtcNow },
                new Recurso { Id = Guid.Parse("a0000000-0000-0000-0000-000000000005"), Codigo = "usuarios.editar", Nome = "Editar Usuários", Descricao = "Permite editar usuários existentes", Ativo = true, DataCriacao = DateTime.UtcNow },
                new Recurso { Id = Guid.Parse("a0000000-0000-0000-0000-000000000006"), Codigo = "usuarios.desativar", Nome = "Desativar Usuários", Descricao = "Permite desativar/ativar usuários", Ativo = true, DataCriacao = DateTime.UtcNow },
                new Recurso { Id = Guid.Parse("a0000000-0000-0000-0000-000000000007"), Codigo = "base_conhecimento.acessar", Nome = "Acessar Base de Conhecimento", Descricao = "Permite acessar a Base de Conhecimento", Ativo = true, DataCriacao = DateTime.UtcNow }
            };

            modelBuilder.Entity<Recurso>().HasData(recursos);

            var perfisRecursos = new List<PerfilAcessoRecurso>();

            // Admin recebe todos
            int i = 1;
            foreach (var r in recursos)
            {
                perfisRecursos.Add(new PerfilAcessoRecurso { Id = Guid.Parse($"b0000000-0000-0000-0000-00000000000{i++}"), PerfilAcessoId = adminPerfilId, RecursoId = r.Id, DataCriacao = DateTime.UtcNow });
            }

            // Operador recebe chat e conversas
            perfisRecursos.Add(new PerfilAcessoRecurso { Id = Guid.Parse($"b0000000-0000-0000-0000-000000000101"), PerfilAcessoId = operadorPerfilId, RecursoId = Guid.Parse("a0000000-0000-0000-0000-000000000001"), DataCriacao = DateTime.UtcNow });
            perfisRecursos.Add(new PerfilAcessoRecurso { Id = Guid.Parse($"b0000000-0000-0000-0000-000000000102"), PerfilAcessoId = operadorPerfilId, RecursoId = Guid.Parse("a0000000-0000-0000-0000-000000000002"), DataCriacao = DateTime.UtcNow });

            // Somente Chat recebe chat
            perfisRecursos.Add(new PerfilAcessoRecurso { Id = Guid.Parse($"b0000000-0000-0000-0000-000000000201"), PerfilAcessoId = somenteChatPerfilId, RecursoId = Guid.Parse("a0000000-0000-0000-0000-000000000001"), DataCriacao = DateTime.UtcNow });

            modelBuilder.Entity<PerfilAcessoRecurso>().HasData(perfisRecursos);
        }
    }
}
