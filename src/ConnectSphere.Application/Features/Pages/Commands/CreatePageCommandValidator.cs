using ConnectSphere.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Pages.Commands
{
    public class CreatePageCommandValidator : AbstractValidator<CreatePageCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreatePageCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.");

            RuleFor(x => x.Url)
                .NotEmpty()
                .WithMessage("Url is required.");
        }

        private async Task<bool> UserExists(long userId, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(x => x.Id == userId, cancellationToken);
        }
    }
} 