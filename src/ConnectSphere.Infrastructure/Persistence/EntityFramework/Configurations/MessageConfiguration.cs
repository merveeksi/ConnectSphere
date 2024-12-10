using ConnectSphere.Domain.Entities;
using ConnectSphere.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConnectSphere.Infrastructure.Persistence.EntityFramework.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("messages");

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

        builder.Property(x => x.SentAt)
            .IsRequired()
            .HasColumnName("sent_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.IsRead)
            .IsRequired()
            .HasColumnName("is_read");

        // Relationships
        builder.HasOne(x => x.Sender)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Receiver)
            .WithMany()
            .HasForeignKey(x => x.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Group)
            .WithMany(x => x.Messages)
            .HasForeignKey(x => x.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        // İndeksler
        builder.HasIndex(x => x.SenderId)
            .HasDatabaseName("ix_messages_sender_id");

        builder.HasIndex(x => x.ReceiverId)
            .HasDatabaseName("ix_messages_receiver_id");

        builder.HasIndex(x => x.SentAt)
            .HasDatabaseName("ix_messages_sent_at");

        // PostgreSQL-specific özellikler
        builder.HasComment("Mesajları içeren tablo");
    }
} 