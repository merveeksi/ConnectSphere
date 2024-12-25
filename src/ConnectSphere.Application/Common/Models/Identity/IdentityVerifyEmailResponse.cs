namespace ConnectSphere.Application.Common.Models.Identity;

public class IdentityVerifyEmailResponse
{
    public IdentityVerifyEmailResponse(string email)
    {
        Email = email;
    }

    public string Email { get; set; }
}