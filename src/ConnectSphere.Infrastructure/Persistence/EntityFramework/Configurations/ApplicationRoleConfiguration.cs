using ConnectSphere.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConnectSphere.Infrastructure.Persistence.EntityFramework.Configurations;

public sealed class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        // Maps to the AspNetRoles table
        builder.ToTable("application_roles");

        // Primary key
        builder.HasKey(r => r.Id);

        // Index for "normalized" role name to allow efficient lookups
        builder.HasIndex(r => r.NormalizedName)
            .HasDatabaseName("RoleNameIndex")
            .IsUnique();

        // A concurrency token for use with the optimistic concurrency checking
        builder.Property(r => r.ConcurrencyStamp)
            .IsConcurrencyToken();

        // Limit the size of columns to use efficient database types
        builder.Property(r => r.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(r => r.NormalizedName)
            .HasMaxLength(100);

        // The relationships between Role and other entity types
        // Each Role can have many entries in the UserRole join table
        builder.HasMany<ApplicationUserRole>()
            .WithOne()
            .HasForeignKey(ur => ur.RoleId)
            .IsRequired();

        // Each Role can have many associated RoleClaims
        builder.HasMany<ApplicationRoleClaim>()
            .WithOne()
            .HasForeignKey(rc => rc.RoleId)
            .IsRequired();
    }
} 