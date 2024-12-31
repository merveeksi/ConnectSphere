using ConnectSphere.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Stories.Commands
{
    public class CreateStoryCommandValidator : AbstractValidator<CreateStoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateStoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Content is required.");

            RuleFor(x => x.MediaUrl)
                .NotEmpty()
                .WithMessage("MediaUrl is required.");

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("Invalid StoryType value.");

            RuleFor(x => x.ExpirationTime)
                .NotEmpty()
                .WithMessage("ExpirationTime is required.")
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("ExpirationTime must be in the future.");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId is required.")
                .MustAsync(UserExists)
                .WithMessage("User does not exist.");
        }

        private async Task<bool> UserExists(long userId, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(x => x.Id == userId, cancellationToken);
        }
    }
} 