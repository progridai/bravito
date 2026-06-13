using Bravito.Domain.Knowledge.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bravito.Infrastructure.Data.Mappings;

public class KnowledgeDocumentMapping : IEntityTypeConfiguration<KnowledgeDocument>
{
    public void Configure(EntityTypeBuilder<KnowledgeDocument> builder)
    {
        builder.ToTable("knowledge_documents");

        builder.HasKey(k => k.Id);

        builder.Property(k => k.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(k => k.App)
            .HasColumnName("app")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(k => k.FileName)
            .HasColumnName("file_name")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(k => k.FilePath)
            .HasColumnName("file_path")
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(k => k.FileHash)
            .HasColumnName("file_hash")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(k => k.MimeType)
            .HasColumnName("mime_type")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(k => k.FileSize)
            .HasColumnName("file_size")
            .IsRequired();

        builder.Property(k => k.Status)
            .HasColumnName("status")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(k => k.ChunkCount)
            .HasColumnName("chunk_count")
            .IsRequired();

        builder.Property(k => k.UploadedAt)
            .HasColumnName("uploaded_at")
            .IsRequired();

        builder.Property(k => k.ProcessedAt)
            .HasColumnName("processed_at");

        builder.Property(k => k.ErrorMessage)
            .HasColumnName("error_message")
            .HasColumnType("text");

        builder.Property(k => k.DeletedAt)
            .HasColumnName("deleted_at");
    }
}
