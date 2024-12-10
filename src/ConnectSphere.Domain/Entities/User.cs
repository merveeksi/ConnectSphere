using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.ValueObjects;
using TSID.Creator.NET;

namespace ConnectSphere.Domain.Entities;

public sealed class User : EntityBase<long>
{
    public UserName Username { get; private set; }
    public FullName FullName { get; private set; }
    public Email Email { get; private set; }
    public PasswordHash PasswordHash { get; private set; }
    public string ProfilePictureUrl { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    public string Role { get; private set; }
    public bool IsActive { get; private set; }

    // Navigations
    public ICollection<Message> Messages { get; private set; } = new List<Message>();
    public ICollection<Group> Groups { get; private set; } = new List<Group>();
    public ICollection<Notification> Notifications { get; private set; } = new List<Notification>();

    public static User Create(UserName username, FullName fullname, Email email, string password, string profilePictureUrl)
    {
        if (username == null) 
            throw new ArgumentNullException(nameof(username));
        if (fullname == null) 
            throw new ArgumentNullException(nameof(fullname));
        if (email == null) 
            throw new ArgumentNullException(nameof(email));
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));
        if (string.IsNullOrWhiteSpace(profilePictureUrl))
            throw new ArgumentException("Profile picture URL cannot be empty.", nameof(profilePictureUrl));

        var user = new User()
        {
            Id = TsidCreator.GetTsid().ToLong(),
            Username = username,
            FullName = fullname,
            Email = email,
            PasswordHash = PasswordHash.Create(password),
            ProfilePictureUrl = profilePictureUrl,
            CreatedAt = DateTime.UtcNow,
            IsActive = true,
            Role = "User"
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
            Username = username;
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
