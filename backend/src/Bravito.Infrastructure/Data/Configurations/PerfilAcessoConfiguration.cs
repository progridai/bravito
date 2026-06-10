using Bravito.Domain.Acesso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bravito.Infrastructure.Data.Configurations
{
    public class PerfilAcessoConfiguration : IEntityTypeConfiguration<PerfilAcesso>
    {
        public void Configure(EntityTypeBuilder<PerfilAcesso> builder)
        {
            builder.ToTable("perfis_acesso");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.Nome).HasColumnName("nome").IsRequired().HasMaxLength(255);
            builder.Property(c => c.Descricao).HasColumnName("descricao").HasMaxLength(1000);
            builder.Property(c => c.Ativo).HasColumnName("ativo");
            builder.Property(c => c.DataCriacao).HasColumnName("data_criacao");
            builder.Property(c => c.DataAlteracao).HasColumnName("data_alteracao");

            builder.HasIndex(c => c.Nome).IsUnique();
        }
    }
}
