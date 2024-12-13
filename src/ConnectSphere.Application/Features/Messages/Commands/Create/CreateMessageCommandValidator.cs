using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.ValueObjects;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Messages.Commands.Create
{
    public sealed class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateMessageCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.SenderId)
                .NotEmpty().WithMessage("Sender ID is required.")
                .MustAsync(UserExists).WithMessage("Sender user does not exist.");

            RuleFor(x => x.ReceiverId)
                .NotEmpty().WithMessage("Receiver ID is required.")
                .MustAsync(UserExists).WithMessage("Receiver user does not exist.");

            RuleFor(x => x.Content)
                .NotNull().WithMessage("Content is required.")
                .Must(BeValidContent).WithMessage("Content must be valid.");

            RuleFor(x => x.SentAt)
                .NotEmpty().WithMessage("SentAt is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("SentAt cannot be in the future.");  
        }

        private async Task<bool> UserExists(long id, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(x => x.Id == id, cancellationToken);
        }

        private bool BeValidContent(Content content)
        {
            if (content == null || string.IsNullOrWhiteSpace(content.Value))
            {
                return false; // Content boş veya null
            }

            if (content.Value.Length > 500)
            {
                return false; // Content uzunluğu 500 karakterden fazla
            }

            if (ContainsProhibitedWords(content.Value))
            {
                return false; // Content yasaklı kelimeler içeriyor
            }

            return true; // Geçerli content
        }

        private bool ContainsProhibitedWords(string contentValue)
        {
            var prohibitedWords = new[] { "spam", "prohibited", "banned" }; // Örnek yasaklı kelimeler
            return prohibitedWords.Any(word => contentValue.ToLower().Contains(word));
        }
    }
}
