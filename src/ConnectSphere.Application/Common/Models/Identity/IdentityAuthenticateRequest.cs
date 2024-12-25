namespace ConnectSphere.Application.Common.Models.Identity;

public sealed class IdentityAuthenticateRequest
{
    public IdentityAuthenticateRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; }
    public string Password { get; set; }
}