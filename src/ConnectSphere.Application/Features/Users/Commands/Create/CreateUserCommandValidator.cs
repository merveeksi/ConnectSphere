using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.ValueObjects;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Users.Commands.Create;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IApplicationDbContext _context;
    private static readonly string[] ProhibitedUsernames = { "admin", "root", "system", "moderator" };

    public CreateUserCommandValidator(IApplicationDbContext context)
    {
        _context = context;


        //Düzeltilmesi gerekiyor

        //     
        //     RuleFor(x => x.UserName)
        //         .NotEmpty().WithMessage("Username is required.")
        //         .Must(BeValidUsername).WithMessage("Username is not valid.")
        //         .MustAsync(BeUniqueUsername).WithMessage("Username already exists.")
        //         .Must(username => !ProhibitedUsernames.Contains(username.Value.ToLower()))
        //         .WithMessage("The specified username is prohibited.");
        //     
        //     RuleFor(x => x.FullName)
        //         .NotEmpty().WithMessage("Full name is required.")
        //         .Must(BeValidFullName).WithMessage("Full name is not valid.");
        //     
        //     RuleFor(x => x.Email)
        //         .NotEmpty().WithMessage("Email is required.")
        //         .Must(BeValidEmail).WithMessage("Email is not valid.")
        //         .MustAsync(BeUniqueEmail).WithMessage("Email already exists.");
        //
        //     RuleFor(x => x.PasswordHash)
        //         .NotEmpty().WithMessage("Password is required.")
        //         .Must(BeValidPassword).WithMessage("Password must meet security requirements.");
        //
        //     RuleFor(x => x.ProfilePictureUrl)
        //         .Must(BeValidUrl)
        //         .When(x => !string.IsNullOrEmpty(x.ProfilePictureUrl))
        //         .WithMessage("Invalid profile picture URL.");
        //
        //     RuleFor(x => x.Role)
        //         .IsInEnum().WithMessage("Invalid role type.");
        //
        //     RuleFor(x => x.CreatedAt)
        //         .NotEmpty().WithMessage("CreatedAt is required.")
        //         .Must(date => date <= DateTime.UtcNow)
        //         .WithMessage("CreatedAt cannot be in the future.");
        //
        //     RuleFor(x => x.LastLoginAt)
        //         .Must(BeValidLastLoginTime)
        //         .When(x => x.LastLoginAt.HasValue)
        //         .WithMessage("Last login time cannot be in the future.");
        // }
        //
        // private async Task<bool> BeUniqueUsername(UserName userName, CancellationToken cancellationToken)
        // {
        //     if (userName?.Value == null) return false;
        //     return !await _context.Users
        //         .AnyAsync(x => x.UserName.Value.ToLower() == userName.Value.ToLower(), cancellationToken);
        // }
        //
        // private async Task<bool> BeUniqueEmail(Email email, CancellationToken cancellationToken)
        // {
        //     if (email?.Value == null) return false;
        //     return !await _context.Users
        //         .AnyAsync(x => x.Email.Value.ToLower() == email.Value.ToLower(), cancellationToken);
        // }
        //
        // private bool BeValidUsername(UserName userName)
        // {
        //     return userName != null && UserName.IsValid(userName.Value);
        // }
        //
        // private bool BeValidFullName(FullName fullName)
        // {
        //     return fullName != null && 
        //            FullName.IsValid(fullName.FirstName) && 
        //            FullName.IsValid(fullName.LastName);
        // }
        //
        // private bool BeValidEmail(Email email)
        // {
        //     return email != null && Email.IsValid(email.Value);
        // }
        //
        // private bool BeValidPassword(PasswordHash passwordHash)
        // {
        //     return passwordHash != null && !string.IsNullOrWhiteSpace(passwordHash.Value);
        // }
        //
        // private bool BeValidUrl(string url)
        // {
        //     if (string.IsNullOrEmpty(url)) return true;
        //     return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
        //         && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        // }
        //
        // private bool BeValidLastLoginTime(DateTime? lastLoginTime)
        // {
        //     return !lastLoginTime.HasValue || lastLoginTime.Value <= DateTime.UtcNow;
        // }
    }
}
