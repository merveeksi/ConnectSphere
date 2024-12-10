using System.Text.RegularExpressions;

namespace ConnectSphere.Domain.ValueObjects;

public sealed record Content
{
    private const int MinLength = 1;
    private const int MaxLength = 1000;
    private static readonly Regex UrlPattern = new(@"(https?:\/\/[^\s]+)", RegexOptions.Compiled);
    private static readonly string[] ForbiddenWords = { "küfür1", "küfür2", "hakaret1" }; // Yasaklı kelimeler

    public string Value { get; private set; }
    public bool ContainsUrl { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Content(string value)
    {
        Value = value;
        ContainsUrl = HasUrl(value);
        CreatedAt = DateTime.UtcNow;
    }

    public static Content Create(string content)
    {
        ValidateContent(content);
        var sanitizedContent = SanitizeContent(content);
        return new Content(sanitizedContent);
    }

    private static void ValidateContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Content cannot be empty.");

        if (content.Length < MinLength)
            throw new ArgumentException($"Content must be at least {MinLength} characters long.");

        if (content.Length > MaxLength)
            throw new ArgumentException($"Content cannot be longer than {MaxLength} characters.");

        if (ContainsForbiddenWords(content))
            throw new ArgumentException("Content contains forbidden words.");
    }

    private static string SanitizeContent(string content)
    {
        // HTML karakterlerini temizle
        content = System.Web.HttpUtility.HtmlEncode(content);
        
        // Fazla boşlukları temizle
        content = Regex.Replace(content, @"\s+", " ");
        
        return content.Trim();
    }

    private static bool ContainsForbiddenWords(string content) // Yasaklı kelimeleri içeriyor mu?
    {
        return ForbiddenWords.Any(word => 
            content.Contains(word, StringComparison.OrdinalIgnoreCase));
    }

    private static bool HasUrl(string content) // URL içeriyor mu?
    {
        return UrlPattern.IsMatch(content);
    }

    public bool ContainsKeyword(string keyword) // Anahtar kelime içeriyor mu?
    {
        return Value.Contains(keyword, StringComparison.OrdinalIgnoreCase);
    }

    public string GetPreview(int maxLength = 50) // Önizleme al
    {
        if (Value.Length <= maxLength)
            return Value;

        return Value.Substring(0, maxLength) + "...";
    }

    public static implicit operator string(Content content) => content.Value;

    public override string ToString() => Value;
}