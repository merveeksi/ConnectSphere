namespace ConnectSphere.Application.Common.Models.Identity;

public sealed class IdentityCreateEmailTokenResponse
{
    public IdentityCreateEmailTokenResponse(string token)
    {
        Token = token;
    }

    public string Token { get; set; }
}