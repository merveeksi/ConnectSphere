namespace ConnectSphere.Application.Common.Models.Identity;

public sealed class IdentityLoginResponse
{
    public IdentityLoginResponse(string token, DateTime expiresAt)
    {
        Token = token;
        ExpiresAt = expiresAt;
    }

    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
}