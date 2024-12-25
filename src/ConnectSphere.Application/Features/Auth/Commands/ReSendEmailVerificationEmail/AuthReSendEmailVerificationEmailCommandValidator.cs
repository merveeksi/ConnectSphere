using ConnectSphere.Application.Common.Interfaces;
using FluentValidation;

namespace ConnectSphere.Application.Features.Auth.Commands.ResedEmail.VerificationEmail;

public sealed class
    AuthReSendEmailVerificationEmailCommandValidator : AbstractValidator<AuthReSendEmailVerificationEmailCommand>
{
    private readonly IIdentityService _identityService;

    public AuthReSendEmailVerificationEmailCommandValidator(IIdentityService identityService)
    {
        _identityService = identityService;
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(CheckEmailExistsAsync)
            .WithMessage("Email address is not exists.")
            .MustAsync(CheckIfEmailVerifiedAsync)
            .WithMessage("Email address is already verified.");
    }

    private Task<bool> CheckEmailExistsAsync(string email, CancellationToken cancellationToken)
    {
        return _identityService.CheckEmailExistsAsync(email, cancellationToken);
    }

    private async Task<bool> CheckIfEmailVerifiedAsync(string email, CancellationToken cancellationToken)
    {
        return !await _identityService.CheckIfEmailVerifiedAsync(email, cancellationToken);
    }
}