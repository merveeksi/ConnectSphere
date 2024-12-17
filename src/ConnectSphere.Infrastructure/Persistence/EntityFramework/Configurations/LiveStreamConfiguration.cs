using ConnectSphere.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConnectSphere.Infrastructure.Persistence.EntityFramework.Configurations;

public class LiveStreamConfiguration : IEntityTypeConfiguration<LiveStream>
{
    public void Configure(EntityTypeBuilder<LiveStream> builder)
    {
        builder.ToTable("live_streams");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn()
            .HasIdentityOptions(startValue: 1);

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired()
            .HasColumnName("title");

        builder.Property(x => x.StreamUrl)
            .HasMaxLength(500)
            .IsRequired()
            .HasColumnName("stream_url");

        builder.Property(x => x.StartedAt)
            .IsRequired()
            .HasColumnName("started_at")
            .HasDefaultValue(DateTime.UtcNow);

        builder.Property(x => x.EndedAt)
            .HasColumnName("ended_at");

        builder.Property(x => x.HostId)
            .IsRequired()
            .HasColumnName("host_id");

        // Messages collection - JSON olarak saklama
        builder.Property(x => x.Messages)
            .HasColumnType("jsonb")
            .HasColumnName("messages");

        // MutedUsers collection - JSON olarak saklama
        builder.Property(x => x.MutedUserIds)
            .HasColumnType("jsonb")
            .HasColumnName("muted_user_ids");

        // Relationships
        builder.HasOne(x => x.Host)
            .WithMany()
            .HasForeignKey(x => x.HostId)
            .OnDelete(DeleteBehavior.Restrict);

        // İndeksler
        builder.HasIndex(x => x.HostId)
            .HasDatabaseName("ix_live_streams_host_id");

        builder.HasIndex(x => x.StartedAt)
            .HasDatabaseName("ix_live_streams_started_at");

        builder.HasIndex(x => x.EndedAt)
            .HasDatabaseName("ix_live_streams_ended_at");

        // Check Constraints
        builder.HasCheckConstraint("CK_LiveStream_EndedAt", 
            "ended_at IS NULL OR ended_at > started_at");

        // PostgreSQL-specific özellikler
        builder.HasComment("Canlı yayın bilgilerini içeren tablo");

        // IsActive computed column
        builder.Property(x => x.IsActive)
            .HasComputedColumnSql("ended_at IS NULL");
    }
} 