using ConnectSphere.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConnectSphere.Infrastructure.Persistence.EntityFramework.Configurations;

public class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.ToTable("media");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn()
            .HasIdentityOptions(startValue: 1);

        builder.Property(x => x.Url)
            .HasMaxLength(500)
            .IsRequired()
            .HasColumnName("url");

        builder.Property(x => x.MediaType)
            .HasMaxLength(50)
            .IsRequired()
            .HasColumnName("media_type");

        builder.Property(x => x.UploadedAt)
            .IsRequired()
            .HasColumnName("uploaded_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Relationships
        builder.HasOne(x => x.UploadedBy)
            .WithMany()
            .HasForeignKey(x => x.UploadedById)
            .OnDelete(DeleteBehavior.Restrict);

        // İndeksler
        builder.HasIndex(x => x.UploadedById)
            .HasDatabaseName("ix_media_uploaded_by_id");

        builder.HasIndex(x => x.MediaType)
            .HasDatabaseName("ix_media_media_type");

        builder.HasIndex(x => x.UploadedAt)
            .HasDatabaseName("ix_media_uploaded_at");

        // PostgreSQL-specific özellikler
        builder.HasComment("Medya dosyalarını içeren tablo");
    }
} 