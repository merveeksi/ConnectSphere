using ConnectSphere.Domain.Entities;
using ConnectSphere.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConnectSphere.Infrastructure.Persistence.EntityFramework.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.ToTable("groups");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn()
            .HasIdentityOptions(startValue: 1);

        builder.Property(x => x.GroupName)
            // Grup adı zorunlu, hasconversion ile value object'e dönüştürüldü
            .HasConversion(
                groupName => groupName.Value,
                value => GroupName.Create(value))
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("group_name");

        builder.Property(x => x.CreatedAt)
            .IsRequired()
            .HasColumnName("created_at")
            .HasDefaultValue(DateTime.UtcNow);

        builder.Property(x => x.CreatedById)
            .IsRequired()
            .HasColumnName("created_by_id");

        // Relationships
        builder.HasOne(x => x.CreatedBy)
            .WithMany()
            .HasForeignKey(x => x.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Members)
            .WithMany(x => x.Groups)
            .UsingEntity(j => 
            {
                j.ToTable("group_members");
                j.Property("GroupsId").HasColumnName("group_id");
                j.Property("MembersId").HasColumnName("member_id");
            });

        builder.HasMany(x => x.Messages)
            .WithOne(x => x.Group)
            .HasForeignKey(x => x.GroupId)
            .OnDelete(DeleteBehavior.Cascade);

        // İndeksler
        builder.HasIndex(x => x.GroupName)
            .IsUnique()
            .HasDatabaseName("ix_groups_group_name")
            .HasFilter("group_name IS NOT NULL");

        builder.HasIndex(x => x.CreatedAt)
            .HasDatabaseName("ix_groups_created_at");

        builder.HasIndex(x => x.CreatedById)
            .HasDatabaseName("ix_groups_created_by_id");

        // PostgreSQL-specific özellikler
        builder.HasComment("Grup bilgilerini içeren tablo");
    }
} 