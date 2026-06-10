using Bravito.Domain.Acesso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bravito.Infrastructure.Data.Configurations
{
    public class UsuarioPerfilAcessoConfiguration : IEntityTypeConfiguration<UsuarioPerfilAcesso>
    {
        public void Configure(EntityTypeBuilder<UsuarioPerfilAcesso> builder)
        {
            builder.ToTable("usuarios_perfis_acesso");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.UsuarioId).HasColumnName("usuario_id").IsRequired();
            builder.Property(c => c.PerfilAcessoId).HasColumnName("perfil_acesso_id").IsRequired();
            builder.Property(c => c.DataCriacao).HasColumnName("data_criacao");

            builder.HasIndex(c => new { c.UsuarioId, c.PerfilAcessoId }).IsUnique();

            builder.HasOne(c => c.Usuario)
                .WithMany(u => u.PerfisAcesso)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.PerfilAcesso)
                .WithMany(p => p.Usuarios)
                .HasForeignKey(c => c.PerfilAcessoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
