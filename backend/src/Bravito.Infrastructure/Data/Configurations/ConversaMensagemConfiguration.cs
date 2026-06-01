using Bravito.Domain.Chat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bravito.Infrastructure.Data.Configurations
{
    public class ConversaMensagemConfiguration : IEntityTypeConfiguration<ConversaMensagem>
    {
        public void Configure(EntityTypeBuilder<ConversaMensagem> builder)
        {
            builder.ToTable("conversas_mensagens");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.ConversaId).HasColumnName("conversa_id").IsRequired();
            builder.Property(c => c.TipoRemetente).HasColumnName("tipo_remetente").IsRequired().HasMaxLength(50);
            builder.Property(c => c.Conteudo).HasColumnName("conteudo").IsRequired();
            builder.Property(c => c.ConteudoBruto).HasColumnName("conteudo_bruto").HasColumnType("jsonb");
            builder.Property(c => c.TokensEntrada).HasColumnName("tokens_entrada");
            builder.Property(c => c.TokensSaida).HasColumnName("tokens_saida");
            builder.Property(c => c.ModeloUsado).HasColumnName("modelo_usado").HasMaxLength(100);
            builder.Property(c => c.DataCriacao).HasColumnName("data_criacao");
            builder.Property(c => c.Status).HasColumnName("status").HasMaxLength(50);

            builder.HasOne(m => m.Conversa)
                .WithMany(c => c.Mensagens)
                .HasForeignKey(m => m.ConversaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
