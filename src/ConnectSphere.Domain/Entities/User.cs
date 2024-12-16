using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.ValueObjects;
using TSID.Creator.NET;

namespace ConnectSphere.Domain.Entities;

public sealed class User : EntityBase<long>
{
    public UserName UserName { get; private set; }
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public string ProfilePictureUrl { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    public string Role { get; private set; } = "User";
    public bool IsActive { get; private set; } = true;

    // Navigations
    public ICollection<Message> Messages { get; private set; } = [];
    public ICollection<Group> Groups { get; private set; } = [];
    public ICollection<Notification> Notifications { get; private set; } = [];

    public static User Create(UserName username, FullName fullname, Email email, PasswordHash passwordHash, string profilePictureUrl, string role)
    {
        if (username == null) 
            throw new ArgumentNullException(nameof(username));
        if (fullname == null) 
            throw new ArgumentNullException(nameof(fullname));
        if (email == null) 
            throw new ArgumentNullException(nameof(email));
        if (passwordHash == null)
            throw new ArgumentNullException(nameof(PasswordHash));
        if (string.IsNullOrWhiteSpace(profilePictureUrl))
            throw new ArgumentException("Profile picture URL cannot be empty.", nameof(profilePictureUrl));
        if (string.IsNullOrWhiteSpace(role))
            throw new ArgumentException("Role cannot be empty.", nameof(role));

        var user = new User()
        {
            Id = TsidCreator.GetTsid().ToLong(),
            UserName = username,
            FullName = fullname,
            Email = email,
            PasswordHash = passwordHash,
            ProfilePictureUrl = profilePictureUrl,
            CreatedAt = DateTime.UtcNow,
            IsActive = true,
            Role = role
        };

        return user;
    }

    public void AssignRole(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            throw new ArgumentException("Role cannot be empty.", nameof(role));

        Role = role;
    }

    public void DeactivateAccount() // hesabı devre dışı bırakma
    {
        if (!IsActive)
            throw new InvalidOperationException("Account is already deactivated.");

        IsActive = false;
    }

    public void ActivateAccount() // hesabı etkinleştirme
    {
        if (IsActive)
            throw new InvalidOperationException("Account is already active.");

        IsActive = true;
    }

    public void UpdatePassword(string newPassword)
    {
        if (string.IsNullOrWhiteSpace(newPassword))
            throw new ArgumentException("New password cannot be empty.", nameof(newPassword));

        PasswordHash = PasswordHash.Create(newPassword);
    }

    public bool VerifyPassword(string password) // Şifre doğrulama metodu
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));

        return PasswordHash.Verify(password);
    }

    public void UpdateProfile(UserName username, FullName fullname, Email email, string profilePictureUrl)
    {
        if (username != null)
            UserName = username;
        if (fullname != null)
            FullName = fullname;
        if (email != null)
            Email = email;
        if (!string.IsNullOrWhiteSpace(profilePictureUrl))
            ProfilePictureUrl = profilePictureUrl;
    }

    public void UpdateLastLoginTime() // Kullanıcı giriş yaptığında çağrılacak
    {
        LastLoginAt = DateTime.UtcNow;
    }
}
