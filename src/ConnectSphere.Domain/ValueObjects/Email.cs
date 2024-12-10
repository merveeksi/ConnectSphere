using System.Text.RegularExpressions;
using ConnectSphere.Domain.Entities;

namespace ConnectSphere.Domain.ValueObjects;

//record değerleri aktarırken değişiklik yapamayız
public record Email
{
    private const string Pattern = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                   @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
    public string Value { get; set; }

    public Email(string value)
    {
        if (!IsValid(value))
            throw new ArgumentException("Invalid email address");

        Value = value;
    }

    private static bool IsValid(string value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        if (!Regex.IsMatch(value, Pattern))
            return false;

        return true;
    }

    public static implicit operator string(Email email) => email.Value; // Email to string

    public static implicit operator Email(string value) => new(value); // string to Email

    public static bool IsTaken(Email email, IEnumerable<User> users)
    {
        if (email == null)
            throw new ArgumentNullException(nameof(email));

        return users.Any(u => u.Email.Value.Equals(email.Value, StringComparison.OrdinalIgnoreCase));
    }

    public void Update(string newValue)
    {
        if (!IsValid(newValue))
            throw new ArgumentException("Invalid email address");

        Value = newValue;
    }
}