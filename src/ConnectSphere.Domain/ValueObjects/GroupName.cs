using System.Text.RegularExpressions;

namespace ConnectSphere.Domain.ValueObjects;

public sealed record GroupName
{
    private const int MinLength = 3;
    private const int MaxLength = 50;
    private static readonly Regex ValidGroupNamePattern = new(@"^[a-zA-Z0-9\s\-_.]+$", RegexOptions.Compiled);

    // Yasaklı kelime kategorileri
    private static readonly string[] ProfanityWords = { "küfür1", "küfür2", "hakaret1" };
    private static readonly string[] PoliticalWords = { "siyasi1", "siyasi2", "propaganda" };
    private static readonly string[] AdultContentWords = { "cinsel1", "müstehcen", "çıplaklık" };
    private static readonly string[] HateWords = { "isyan1", "nefret1", "alay1" };
    private static readonly string[] SpamWords = { "reklam", "casino", "bahis" };

    public string Value { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private GroupName(string value)
    {
        Value = value;
        CreatedAt = DateTime.UtcNow;
    }

    public static GroupName Create(string groupName)
    {
        ValidateGroupName(groupName);
        var sanitizedName = SanitizeGroupName(groupName);
        return new GroupName(sanitizedName);
    }

    private static void ValidateGroupName(string groupName)
    {
        if (string.IsNullOrWhiteSpace(groupName))
            throw new ArgumentException("Group name cannot be empty.");

        var trimmedName = groupName.Trim();

        if (trimmedName.Length < MinLength)
            throw new ArgumentException($"Group name must be at least {MinLength} characters long.");

        if (trimmedName.Length > MaxLength)
            throw new ArgumentException($"Group name cannot be longer than {MaxLength} characters.");

        if (!ValidGroupNamePattern.IsMatch(trimmedName))
            throw new ArgumentException("Group name can only contain letters, numbers, spaces, dots, underscores, and hyphens.");

        ValidateInappropriateContent(trimmedName);
    }

    private static void ValidateInappropriateContent(string groupName)
    {
        var normalizedName = groupName.ToLower();

        // Küfür ve hakaret kontrolü
        if (ContainsAnyWord(normalizedName, ProfanityWords))
            throw new ArgumentException("Group name cannot contain profanity or offensive content.");

        // Siyasi içerik kontrolü
        if (ContainsAnyWord(normalizedName, PoliticalWords))
            throw new ArgumentException("Group name cannot contain political content.");

        // Müstehcen içerik kontrolü
        if (ContainsAnyWord(normalizedName, AdultContentWords))
            throw new ArgumentException("Group name cannot contain adult or inappropriate content.");

        // Nefret söylemi ve alay kontrolü
        if (ContainsAnyWord(normalizedName, HateWords))
            throw new ArgumentException("Group name cannot contain hate speech or mockery.");

        // Spam ve reklam kontrolü
        if (ContainsAnyWord(normalizedName, SpamWords))
            throw new ArgumentException("Group name cannot contain spam or advertising content.");
    }

    private static string SanitizeGroupName(string groupName)
    {
        // Başındaki ve sonundaki boşlukları temizle
        var sanitized = groupName.Trim();

        // Birden fazla boşluğu tek boşluğa indir
        sanitized = Regex.Replace(sanitized, @"\s+", " ");

        // HTML karakterlerini temizle
        sanitized = System.Web.HttpUtility.HtmlEncode(sanitized);

        return sanitized;
    }

    private static bool ContainsAnyWord(string text, string[] words) // Kelimeleri içeriyor mu kontrolü
    {
        return words.Any(word => text.Contains(word, StringComparison.OrdinalIgnoreCase));
    }

    public bool IsDefaultGroup() // Varsayılan grup mu kontrolü
    {
        return Value.Equals("General", StringComparison.OrdinalIgnoreCase) ||
               Value.Equals("Announcements", StringComparison.OrdinalIgnoreCase);
    }

    public string GetGroupInitials(int maxLength = 2)
    {
        var words = Value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        return string.Concat(words.Take(maxLength).Select(w => char.ToUpper(w[0])));
    }

    public static implicit operator string(GroupName groupName) => groupName.Value;

    public override string ToString() => Value;
}