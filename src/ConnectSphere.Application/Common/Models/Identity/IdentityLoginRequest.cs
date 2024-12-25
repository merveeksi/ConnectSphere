namespace ConnectSphere.Application.Common.Models.Identity;

public sealed class IdentityLoginRequest
{
    public IdentityLoginRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; }
    public string Password { get; set; }
}