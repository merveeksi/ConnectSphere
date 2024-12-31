using ConnectSphere.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Posts.Commands
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreatePostCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required.");

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Content is required.");

            RuleFor(x => x.AuthorId)
                .NotEmpty()
                .WithMessage("AuthorId is required.")
                .MustAsync(UserExists)
                .WithMessage("User does not exist.");

            RuleFor(x => x.Type)
                .IsInEnum()
                .WithMessage("Invalid PostType value.");

            RuleFor(x => x.IsActive)
                .NotNull()
                .WithMessage("IsActive must be specified.");
        }

        private async Task<bool> UserExists(long userId, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(x => x.Id == userId, cancellationToken);
        }
    }
} 