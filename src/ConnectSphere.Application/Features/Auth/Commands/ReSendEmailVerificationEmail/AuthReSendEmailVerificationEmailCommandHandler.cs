using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Application.Common.Models.Email;
using ConnectSphere.Application.Common.Models.General;
using ConnectSphere.Application.Common.Models.Identity;
using MediatR;

namespace ConnectSphere.Application.Features.Auth.Commands.ResedEmail.VerificationEmail;

public sealed class AuthReSendEmailVerificationEmailCommandHandler: IRequestHandler<AuthReSendEmailVerificationEmailCommand, ResponseDto<string>>
{
    private readonly IIdentityService _identityService;
    private readonly IEmailService _emailService;
    public AuthReSendEmailVerificationEmailCommandHandler(IIdentityService identityService, IEmailService emailService)
    {
        _identityService = identityService;
        _emailService = emailService;
    }
    public async Task<ResponseDto<string>> Handle(AuthReSendEmailVerificationEmailCommand request, CancellationToken cancellationToken)
    {
        var response = await _identityService.CreateEmailTokenAsync(new IdentityCreateEmailTokenRequest(request.Email), cancellationToken);
        var emailVerificationDto = new EmailVerificationDto(request.Email, response.Token);
        await _emailService.EmailVerificationAsync(emailVerificationDto, cancellationToken);
        return new ResponseDto<string>(data: response.Token, message: "Email verification email sent.");
    }
