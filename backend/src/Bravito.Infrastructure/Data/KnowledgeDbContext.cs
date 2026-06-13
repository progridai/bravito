using Bravito.Domain.Knowledge.Entities;
using Bravito.Infrastructure.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Bravito.Infrastructure.Data;

public class KnowledgeDbContext : DbContext
{
    public DbSet<KnowledgeDocument> KnowledgeDocuments { get; set; } = null!;

    public KnowledgeDbContext(DbContextOptions<KnowledgeDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new KnowledgeDocumentMapping());
    }
}
