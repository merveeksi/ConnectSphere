using ConnectSphere.Application.Common.Interfaces;
using FluentValidation;

namespace ConnectSphere.Application.Features.Auth.Commands.Register;

public sealed class AuthRegisterCommandValidator : AbstractValidator<AuthRegisterCommand>
{
    private readonly IIdentityService _identityService;

    public AuthRegisterCommandValidator(IIdentityService identityService)
    {
        _identityService = identityService;
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
        RuleFor(x => x.FirstName)
            .MaximumLength(50);
        RuleFor(x => x.LastName)
            .MaximumLength(50);
        RuleFor(x => x.Email)
            .MustAsync(CheckEmailExistsAsync)
            .WithMessage("The email is already in use.");
    }

    private async Task<bool> CheckEmailExistsAsync(string email, CancellationToken cancellationToken)
    {
        return !await _identityService.CheckEmailExistsAsync(email, cancellationToken);
    }
}