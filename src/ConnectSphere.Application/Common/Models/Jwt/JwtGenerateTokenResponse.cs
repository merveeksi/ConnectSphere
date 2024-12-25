namespace ConnectSphere.Application.Common.Models.Jwt;

public sealed class JwtGenerateTokenResponse
{
    public JwtGenerateTokenResponse(string token, DateTime expiresAt)
    {
        Token = token;
        ExpiresAt = expiresAt;
    }
    
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
}