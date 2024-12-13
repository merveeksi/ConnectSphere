using ConnectSphere.Domain.Entities;
using ConnectSphere.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConnectSphere.Infrastructure.Persistence.EntityFramework.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn()
            .HasIdentityOptions(startValue: 1);

        builder.Property(x => x.UserName)
            .HasConversion(
                x => x.Value,
                x => UserName.Create(x))
            .IsRequired()
            .HasColumnName("username");

        builder.Property(x => x.Email)
            .HasConversion(
                x => x.Value,
                x => new Email(x))
            .IsRequired()
            .HasColumnName("email");

        builder.Property(x => x.PasswordHash)
            .HasConversion(
                x => x.Value,
                x => PasswordHash.Create(x))
            .IsRequired()
            .HasColumnName("password_hash");

        builder.OwnsOne(x => x.FullName, fnBuilder =>
        {
            fnBuilder.Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("first_name");

            fnBuilder.Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired()
                .HasColumnName("last_name");
        });

        builder.Property(x => x.ProfilePictureUrl)
            .HasMaxLength(500)
            .HasColumnName("profile_picture_url");

        builder.Property(x => x.Role)
            .HasMaxLength(50)
            .IsRequired()
            .HasColumnName("role");

        // Relationships
        builder.HasMany(x => x.Messages)
            .WithOne(x => x.Sender)
            .HasForeignKey(x => x.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.Groups)
            .WithMany(x => x.Members)
            .UsingEntity(j => j.ToTable("group_members"));

        builder.HasMany(x => x.Notifications)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // İndeksler
        builder.HasIndex(x => x.UserName)
            .IsUnique()
            .HasDatabaseName("ix_users_username");

        builder.HasIndex(x => x.Email)
            .IsUnique()
            .HasDatabaseName("ix_users_email");
    }
} 