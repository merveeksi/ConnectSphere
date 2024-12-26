using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Application.Common.Models.General;
using ConnectSphere.Application.Common.Models.Identity;
using ConnectSphere.Application.Features.Auth.Commands.VerifyEmail;
using MediatR;

namespace ConnectSphere.Application.Features.Auth.VerifyEmail;

public sealed class AuthVerifyEmailCommandHandler : IRequestHandler<AuthVerifyEmailCommand, ResponseDto<string>>
{
    private readonly IIdentityService _identityService;

    public AuthVerifyEmailCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<ResponseDto<string>> Handle(AuthVerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var response =
            await _identityService.VerifyEmailAsync(new IdentityVerifyEmailRequest(request.Email, request.Token),
                cancellationToken);
        return new ResponseDto<string>(data: response.Email, message: "Email verified successfully.");
    }
}