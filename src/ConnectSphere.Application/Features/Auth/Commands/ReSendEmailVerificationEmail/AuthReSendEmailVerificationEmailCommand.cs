using MediatR;

namespace ConnectSphere.Application.Features.Auth.Commands.ResedEmail.VerificationEmail;

public sealed class AuthReSendEmailVerificationEmailCommand : IRequest<ResponseDto<string>>
{
    public string Email { get; set; }

    public AuthReSendEmailVerificationEmailCommand(string email)
    {
        Email = email;
    }
}