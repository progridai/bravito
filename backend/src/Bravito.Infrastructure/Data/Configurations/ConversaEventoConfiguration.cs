using Bravito.Domain.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bravito.Infrastructure.Data.Configurations
{
    public class ConversaEventoConfiguration : IEntityTypeConfiguration<ConversaEvento>
    {
        public void Configure(EntityTypeBuilder<ConversaEvento> builder)
        {
            builder.ToTable("conversas_eventos");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.ConversaId).HasColumnName("conversa_id").IsRequired();
            builder.Property(c => c.TipoEvento).HasColumnName("tipo_evento").IsRequired().HasMaxLength(100);
            builder.Property(c => c.Detalhes).HasColumnName("detalhes").HasColumnType("jsonb");
            builder.Property(c => c.DataCriacao).HasColumnName("data_criacao");

            builder.HasOne(e => e.Conversa)
                .WithMany(c => c.Eventos)
                .HasForeignKey(e => e.ConversaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
