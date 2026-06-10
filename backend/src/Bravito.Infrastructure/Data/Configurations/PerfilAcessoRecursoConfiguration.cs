using Bravito.Domain.Acesso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bravito.Infrastructure.Data.Configurations
{
    public class PerfilAcessoRecursoConfiguration : IEntityTypeConfiguration<PerfilAcessoRecurso>
    {
        public void Configure(EntityTypeBuilder<PerfilAcessoRecurso> builder)
        {
            builder.ToTable("perfis_acesso_recursos");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(c => c.PerfilAcessoId).HasColumnName("perfil_acesso_id").IsRequired();
            builder.Property(c => c.RecursoId).HasColumnName("recurso_id").IsRequired();
            builder.Property(c => c.DataCriacao).HasColumnName("data_criacao");

            builder.HasIndex(c => new { c.PerfilAcessoId, c.RecursoId }).IsUnique();

            builder.HasOne(c => c.PerfilAcesso)
                .WithMany(p => p.Recursos)
                .HasForeignKey(c => c.PerfilAcessoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Recurso)
                .WithMany(r => r.PerfisAcesso)
                .HasForeignKey(c => c.RecursoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
