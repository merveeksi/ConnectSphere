using ConnectSphere.Application.Common.Models.General;
using MediatR;

namespace ConnectSphere.Application.Features.Auth.Commands.VerifyEmail;

public sealed class AuthVerifyEmailCommand : IRequest<ResponseDto<string>>
{
    public string Email { get; set; }
    public string Token { get; set; }

    public AuthVerifyEmailCommand(string email, string token)
    {
        Email = email;
        Token = token;
    }

}