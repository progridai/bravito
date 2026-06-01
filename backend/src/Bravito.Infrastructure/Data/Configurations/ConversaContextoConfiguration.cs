using Bravito.Domain.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bravito.Infrastructure.Data.Configurations
{
    public class ConversaContextoConfiguration : IEntityTypeConfiguration<ConversaContexto>
    {
        public void Configure(EntityTypeBuilder<ConversaContexto> builder)
        {
            builder.ToTable("conversas_contextos");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.ConversaId).HasColumnName("conversa_id").IsRequired();
            builder.Property(c => c.ResumoAtual).HasColumnName("resumo_atual");
            builder.Property(c => c.DadosAuxiliares).HasColumnName("dados_auxiliares").HasColumnType("jsonb");
            builder.Property(c => c.DataAtualizacao).HasColumnName("data_atualizacao");

            builder.HasOne(c => c.Conversa)
                .WithOne(c => c.Contexto)
                .HasForeignKey<ConversaContexto>(c => c.ConversaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
