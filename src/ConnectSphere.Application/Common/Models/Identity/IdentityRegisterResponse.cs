namespace ConnectSphere.Application.Common.Models.Identity;

public class IdentityRegisterResponse
{
    public IdentityRegisterResponse(Guid id, string email, string emailToken)
    {
        Id = id;
        Email = email;
        EmailToken = emailToken;
    }

    public Guid Id { get; set; }
    public string Email { get; set; }
    public string EmailToken { get; set; }
}