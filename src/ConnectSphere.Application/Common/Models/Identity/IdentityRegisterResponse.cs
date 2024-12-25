namespace ConnectSphere.Application.Common.Models.Identity;

public class IdentityRegisterResponse
{
    public IdentityRegisterResponse(Guid id, string emailToken)
    {
        Id = id;
        EmailToken = emailToken;
    }

    public Guid Id { get; set; }
    public string EmailToken { get; set; }
}