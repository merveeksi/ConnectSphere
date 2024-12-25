namespace ConnectSphere.Domain.Settings;

public sealed class JwtSettings
{
    public string SecretKey { get; set; }
    public TimeSpan AccessTokenExpiration { get; set; }
    public TimeSpan RefreshTokenExpiration { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}