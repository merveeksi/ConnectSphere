using System.Security.Cryptography;
using System.Text;

namespace ConnectSphere.Domain.ValueObjects;

public sealed record PasswordHash
{
    private const int MinLength = 8;
    private const int MaxLength = 100;
    private const int SaltSize = 16; // 128 bit
    private const int HashSize = 32; // 256 bit
    private const int Iterations = 10000;

    public string Value { get; private set; }
    private byte[] Salt { get; set; }

    private PasswordHash(string hashedPassword, byte[] salt)
    {
        Value = hashedPassword;
        Salt = salt;
    }

    public static PasswordHash Create(string password)
    {
        ValidatePassword(password);

        // Generate a random salt
        byte[] salt = GenerateSalt();
        
        // Hash the password with the salt
        string hashedPassword = HashPassword(password, salt);

        return new PasswordHash(hashedPassword, salt);
    }

    public bool Verify(string password) // Kullanıcı girişinde şifre doğrulama
    {
        var hashedPassword = HashPassword(password, Salt);
        return Value.Equals(hashedPassword);
    }

    private static void ValidatePassword(string password) // Şifre doğrulama kuralları
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.");

        if (password.Length < MinLength)
            throw new ArgumentException($"Password must be at least {MinLength} characters long.");

        if (password.Length > MaxLength)
            throw new ArgumentException($"Password cannot be longer than {MaxLength} characters.");

        // En az bir büyük harf
        if (!password.Any(char.IsUpper))
            throw new ArgumentException("Password must contain at least one uppercase letter.");

        // En az bir küçük harf
        if (!password.Any(char.IsLower))
            throw new ArgumentException("Password must contain at least one lowercase letter.");

        // En az bir rakam
        if (!password.Any(char.IsDigit))
            throw new ArgumentException("Password must contain at least one digit.");

        // En az bir özel karakter
        if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            throw new ArgumentException("Password must contain at least one special character.");
    }

    private static byte[] GenerateSalt() // Random salt oluşturma
    {
        byte[] salt = new byte[SaltSize];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    private static string HashPassword(string password, byte[] salt) // Şifreleme
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
        {
            byte[] hash = pbkdf2.GetBytes(HashSize);
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
            return Convert.ToBase64String(hashBytes);
        }
    }

    public static implicit operator string(PasswordHash passwordHash) => passwordHash.Value;

    public override string ToString() => "[PROTECTED]"; // Güvenlik için hash'i gösterme
}