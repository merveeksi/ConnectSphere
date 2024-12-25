namespace ConnectSphere.Application.Common.Models.Identity;

public sealed class IdentityCreateEmailTokenRequest
{
    public IdentityCreateEmailTokenRequest(string email)
    {
        Email = email;
    }

    public string Email { get; set; }
}