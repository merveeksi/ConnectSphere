using System.Text.RegularExpressions;
using ConnectSphere.Domain.Entities;

namespace ConnectSphere.Domain.ValueObjects;

public sealed record UserName
{
    private const int MinLength = 3;
    private const int MaxLength = 20;
    private static readonly Regex ValidUsernamePattern = new("^[a-zA-Z0-9._-]+$", RegexOptions.Compiled);

    public string Value { get; set; }

    private UserName(string value)
    {
        Value = value;
    }

    public static UserName Create(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Username cannot be empty.", nameof(username));

        var trimmedUsername = username.Trim();

        if (trimmedUsername.Length < MinLength)
            throw new ArgumentException($"Username must be at least {MinLength} characters long.", nameof(username));

        if (trimmedUsername.Length > MaxLength)
            throw new ArgumentException($"Username cannot be longer than {MaxLength} characters.", nameof(username));

        if (!ValidUsernamePattern.IsMatch(trimmedUsername))
            throw new ArgumentException("Username can only contain letters, numbers, dots, underscores, and hyphens.", nameof(username));

        return new UserName(trimmedUsername);
    }

    public static implicit operator string(UserName username) => username.Value;

    public override string ToString() => Value;

    public static bool IsValid(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return false;

        var trimmedUsername = username.Trim();

        if (trimmedUsername.Length < MinLength || trimmedUsername.Length > MaxLength)
            return false;

        return ValidUsernamePattern.IsMatch(trimmedUsername);
    }

    public static bool IsTaken(UserName username, IEnumerable<User> users)
    {
        if (username == null)
            throw new ArgumentNullException(nameof(username));

        return users.Any(u => u.UserName.Value.Equals(username.Value, StringComparison.OrdinalIgnoreCase));
    }

    public void Update(string newValue)
    {
        var newUsername = Create(newValue);
        Value = newUsername.Value;
    }
}