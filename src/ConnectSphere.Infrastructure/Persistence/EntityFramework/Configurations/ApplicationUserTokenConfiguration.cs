using ConnectSphere.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConnectSphere.Infrastructure.Persistence.EntityFramework.Configurations;

public sealed class ApplicationUserTokenConfiguration : IEntityTypeConfiguration<ApplicationUserToken>
{
    public void Configure(EntityTypeBuilder<ApplicationUserToken> builder)
    {
        builder.ToTable("aplication_user_tokens");
        builder.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.LoginProvider).HasMaxLength(191);
        builder.Property(x => x.Name).HasMaxLength(191);
        builder.Property(x => x.Value);
    }
} 