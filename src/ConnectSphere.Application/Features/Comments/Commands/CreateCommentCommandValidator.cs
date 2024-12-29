using ConnectSphere.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Comments.Commands
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateCommentCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.PostId)
                .NotEmpty()
                .WithMessage("PostId is required.")
                .MustAsync(PostExists)
                .WithMessage("Post does not exist.");

            RuleFor(x => x.Post)
                .NotEmpty()
                .WithMessage("Post is required.");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId is required.")
                .MustAsync(UserExists)
                .WithMessage("User does not exist.");

            RuleFor(x => x.User)
                .NotEmpty()
                .WithMessage("User is required.");

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Content is required.");
        }

        private async Task<bool> PostExists(long postId, CancellationToken cancellationToken)
        {
            return await _context.Posts.AnyAsync(x => x.Id == postId, cancellationToken);
        }

        private async Task<bool> UserExists(long userId, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(x => x.Id == userId, cancellationToken);
        }
    }
} 