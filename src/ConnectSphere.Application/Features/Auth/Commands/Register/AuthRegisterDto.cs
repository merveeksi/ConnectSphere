using ConnectSphere.Application.Common.Models.Identity;

namespace ConnectSphere.Application.Features.Auth.Commands.Register;

public sealed class AuthRegisterDto
{
    public long UserId { get; set; }
    public string EmailToken { get; set; }
    public AuthRegisterDto(long userId, string emailToken)
    {
        UserId = userId;
        EmailToken = emailToken;
    }
    public static AuthRegisterDto Create(IdentityRegisterResponse response)
    {
        return new AuthRegisterDto(response.Id, response.EmailToken);
    } 
}