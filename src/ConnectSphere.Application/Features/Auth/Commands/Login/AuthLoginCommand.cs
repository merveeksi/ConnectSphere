using ConnectSphere.Application.Common.Models.Identity;
using MediatR;

namespace ConnectSphere.Application.Features.Auth.Commands.Login;

public sealed class AuthLoginCommand: IRequest<ResponseDto<AuthLoginDto>>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public AuthLoginCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }
    public IdentityLoginRequest ToIdentityLoginRequest()
    {
        return new IdentityLoginRequest(Email, Password);
    }
    public IdentityAuthenticateRequest ToIdentityAuthenticateRequest()
    {
        return new IdentityAuthenticateRequest(Email, Password);
    }
}
