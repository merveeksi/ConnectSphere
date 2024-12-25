using ConnectSphere.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConnectSphere.Infrastructure.Persistence.Seeders;

public class UserSeeders: IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        long initalUserId = long.Parse("");

        var initialUser = new ApplicationUser
        {
            Id = initalUserId,
            UserName = "merve",
            NormalizedUserName = "MERVE",
            Email = "merveeksii61@gmail.com",
            NormalizedEmail = "MERVEEKSII61@GMAİL.COM",
            EmailConfirmed = true,
            CreatedByUserId = initalUserId.ToString(),
            CreatedOn = new DateTimeOffset(new DateTime(2024, 08, 28)),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            SecurityStamp = Guid.NewGuid().ToString(),
            FirstName = "Merve",
            LastName = "Eksi",
            LockoutEnabled = false,
            AccessFailedCount = 0
        };

        initialUser.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(initialUser, "123merve123");

        builder.HasData(initialUser);
    }

}