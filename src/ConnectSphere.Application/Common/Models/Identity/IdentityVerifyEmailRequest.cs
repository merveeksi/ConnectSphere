namespace ConnectSphere.Application.Common.Models.Identity;

public sealed class IdentityVerifyEmailRequest
{
    public IdentityVerifyEmailRequest(string email, string token)
    {
        Email = email;
        Token = token;
    }

    public string Email { get; set; }
    public string Token { get; set; }
}