using ConnectSphere.Domain.Entities;
using ConnectSphere.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConnectSphere.Infrastructure.Persistence.EntityFramework.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.ToTable("notifications");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn()
            .HasIdentityOptions(startValue: 1);

        builder.Property(x => x.Content)
            .HasConversion(
                x => x.Value,
                x => Content.Create(x))
            .IsRequired()
            .HasColumnName("content");

        builder.Property(x => x.IsRead)
            .IsRequired()
            .HasColumnName("is_read");

        builder.Property(x => x.SentAt)
            .IsRequired()
            .HasColumnName("sent_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.NotificationType)
            .HasMaxLength(50)
            .IsRequired()
            .HasColumnName("notification_type");

        // Relationships
        builder.HasOne(x => x.User)
            .WithMany(x => x.Notifications)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // İndeksler
        builder.HasIndex(x => x.UserId)
            .HasDatabaseName("ix_notifications_user_id");

        builder.HasIndex(x => x.SentAt)
            .HasDatabaseName("ix_notifications_sent_at");

        // PostgreSQL-specific özellikler
        builder.HasComment("Bildirimleri içeren tablo");
    }
} 