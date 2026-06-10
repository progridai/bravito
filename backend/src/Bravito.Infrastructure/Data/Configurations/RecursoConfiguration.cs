using Bravito.Domain.Acesso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bravito.Infrastructure.Data.Configurations
{
    public class RecursoConfiguration : IEntityTypeConfiguration<Recurso>
    {
        public void Configure(EntityTypeBuilder<Recurso> builder)
        {
            builder.ToTable("recursos");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.Codigo).HasColumnName("codigo").IsRequired().HasMaxLength(255);
            builder.Property(c => c.Nome).HasColumnName("nome").IsRequired().HasMaxLength(255);
            builder.Property(c => c.Descricao).HasColumnName("descricao").HasMaxLength(1000);
            builder.Property(c => c.Ativo).HasColumnName("ativo");
            builder.Property(c => c.DataCriacao).HasColumnName("data_criacao");
            builder.Property(c => c.DataAlteracao).HasColumnName("data_alteracao");

            builder.HasIndex(c => c.Codigo).IsUnique();
        }
    }
}
