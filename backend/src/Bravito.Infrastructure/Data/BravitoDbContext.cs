using Bravito.Domain.Chat;
using Microsoft.EntityFrameworkCore;
using Bravito.Infrastructure.Data.Configurations;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Aplica as configurações do Fluent API (snake_case mappings já feitos nas classes)
            modelBuilder.ApplyConfiguration(new ConversaConfiguration());
            modelBuilder.ApplyConfiguration(new ConversaMensagemConfiguration());
            modelBuilder.ApplyConfiguration(new ConversaContextoConfiguration());
            modelBuilder.ApplyConfiguration(new ConversaEventoConfiguration());
        }
    }
}
