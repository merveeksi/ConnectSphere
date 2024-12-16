using ConnectSphere.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConnectSphere.Infrastructure.Persistence.EntityFramework.Configurations;

public sealed class ApplicationUserLoginConfiguration : IEntityTypeConfiguration<ApplicationUserLogin>
{
    public void Configure(EntityTypeBuilder<ApplicationUserLogin> builder)
    {
        builder.ToTable("application_user_logins");
        builder.HasKey(x => new { x.LoginProvider, x.UserId });
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.LoginProvider).HasMaxLength(128);
        builder.Property(x => x.ProviderKey).HasMaxLength(128);
    }
} 