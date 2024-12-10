using ConnectSphere.Domain.Common.Entities;
using TSID.Creator.NET;

namespace ConnectSphere.Domain.Entities;

public sealed class User : EntityBase<long>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string ProfilePictureUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public string Role { get; set; }
    public bool IsActive { get; set; }

    // Navigations
    public ICollection<Message> Messages { get; set; }
    public ICollection<Group> Groups { get; set; }
    public ICollection<Notification> Notifications { get; set; }

    public static User Create(string username, string email, string passwordHash, string profilePictureUrl)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty.", nameof(username));
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email cannot be empty.", nameof(email));
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("Password hash cannot be empty.", nameof(passwordHash));
        if (string.IsNullOrWhiteSpace(profilePictureUrl))
            throw new ArgumentException("Profile picture URL cannot be empty.", nameof(profilePictureUrl));

        var user = new User()
        {
            Id = TsidCreator.GetTsid().ToLong(),
            Username = username,
            Email = email,
            PasswordHash = passwordHash,
            ProfilePictureUrl = profilePictureUrl,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        return user;
    }

    public static bool IsUsernameTaken(string username, IEnumerable<User> users) // kullanıcı adı alınmış mı kontrolü
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty.");

        return users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }

    public static bool IsEmailTaken(string email, IEnumerable<User> users) // email alınmış mı kontrolü
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.");

        return users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public void AssignRole(string role) // rol atama
    {
        Role = role;
    }

    public void DeactivateAccount() // hesabı pasif hale getirme
    {
        IsActive = false;
    }

    public void UpdateUsername(string newUsername) // kullanıcı adı güncelleme
    {
        if (string.IsNullOrWhiteSpace(newUsername))
            throw new ArgumentException("New username cannot be empty.", nameof(newUsername));

        Username = newUsername;
    }

    public void UpdateEmail(string newEmail) // email güncelleme
    {
        if (string.IsNullOrWhiteSpace(newEmail))
            throw new ArgumentException("New email cannot be empty.", nameof(newEmail));

        Email = newEmail;
    }

    public void UpdatePassword(string newPasswordHash) // şifre güncelleme
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentException("New password hash cannot be empty.", nameof(newPasswordHash));

        PasswordHash = newPasswordHash;
    }

    public void UpdateProfile(string username, string email, string profilePictureUrl) // profil güncelleme
    {
        if (!string.IsNullOrWhiteSpace(username))
            Username = username;
        if (!string.IsNullOrWhiteSpace(email))
            Email = email;
        if (!string.IsNullOrWhiteSpace(profilePictureUrl))
            ProfilePictureUrl = profilePictureUrl;
    }
}
