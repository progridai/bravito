using Bravito.Domain.Acesso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bravito.Infrastructure.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuarios");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.KeycloakId).HasColumnName("keycloak_id").IsRequired().HasMaxLength(255);
            builder.Property(c => c.Nome).HasColumnName("nome").IsRequired().HasMaxLength(255);
            builder.Property(c => c.Email).HasColumnName("email").IsRequired().HasMaxLength(255);
            builder.Property(c => c.Ativo).HasColumnName("ativo");
            builder.Property(c => c.DataCriacao).HasColumnName("data_criacao");
            builder.Property(c => c.DataAlteracao).HasColumnName("data_alteracao");

            builder.HasIndex(c => c.KeycloakId).IsUnique();
            builder.HasIndex(c => c.Email);
        }
    }
}
