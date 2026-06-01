using Bravito.Domain.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bravito.Infrastructure.Data.Configurations
{
    public class ConversaConfiguration : IEntityTypeConfiguration<Conversa>
    {
        public void Configure(EntityTypeBuilder<Conversa> builder)
        {
            builder.ToTable("conversas");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.UsuarioId).HasColumnName("usuario_id").IsRequired().HasMaxLength(100);
            builder.Property(c => c.IdentificadorExterno).HasColumnName("identificador_externo").HasMaxLength(100);
            builder.Property(c => c.CanalOrigem).HasColumnName("canal_origem").HasMaxLength(50);
            builder.Property(c => c.Status).HasColumnName("status").HasMaxLength(50);
            builder.Property(c => c.DataCriacao).HasColumnName("data_criacao");
            builder.Property(c => c.DataUltimaInteracao).HasColumnName("data_ultima_interacao");
            builder.Property(c => c.Metadados).HasColumnName("metadados").HasColumnType("jsonb");

            builder.HasIndex(c => c.UsuarioId);
        }
    }
}
