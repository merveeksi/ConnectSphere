namespace ConnectSphere.Application.Common.Models.Identity;

public sealed class IdentityRegisterRequest
{
    public IdentityRegisterRequest(string email, string password, string firstName, string lastName, string role)
    {
        Email = email;
        Password = password;
        FirstName = firstName;
        LastName = lastName;
        Role = role;
    }

    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
}