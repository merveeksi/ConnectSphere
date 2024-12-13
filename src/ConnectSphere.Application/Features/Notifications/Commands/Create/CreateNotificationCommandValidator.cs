using System;
using System.Threading;
using System.Threading.Tasks;
using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.ValueObjects;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Notifications.Commands.Create
{
    public sealed class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateNotificationCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.")
                .MustAsync(UserExists).WithMessage("User does not exist.");

            RuleFor(x => x.Content)
                .NotNull().WithMessage("Content is required.")
                .Must(BeValidContent).WithMessage("Content must be valid.");

            RuleFor(x => x.NotificationType)
                .NotEmpty().WithMessage("Notification type is required.");

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

            return true; // Geçerli content
        }
    }
}