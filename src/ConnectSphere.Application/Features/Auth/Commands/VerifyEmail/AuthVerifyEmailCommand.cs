using MediatR;

namespace ConnectSphere.Application.Features.Auth.VerifyEmail;

public sealed class : IRequest<ResponseDto<string>>
{
    public string Email { get; set; }
    public string Token { get; set; }

    public AuthVerifyEmailCommand(string email, string token)
    {
        Email = email;
        Token = token;
    }
}