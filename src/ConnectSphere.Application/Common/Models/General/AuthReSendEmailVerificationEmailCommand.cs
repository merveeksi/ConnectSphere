using MediatR;

namespace ConnectSphere.Application.Common.Models.General;

public sealed class AuthReSendEmailVerificationEmailCommand : IRequest<ResponseDto<string>>
{
    public AuthReSendEmailVerificationEmailCommand(string email)
    {
        Email = email;
    }

    public string Email { get; set; }
}