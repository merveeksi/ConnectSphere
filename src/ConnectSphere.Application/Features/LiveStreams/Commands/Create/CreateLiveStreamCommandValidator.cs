using ConnectSphere.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.LiveStreams.Commands.Create
{
    public sealed class CreateLiveStreamCommandValidator : AbstractValidator<CreateLiveStreamCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateLiveStreamCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.HostId)
                .NotEmpty().WithMessage("Host ID is required.")
                .MustAsync(UserExists).WithMessage("Host user does not exist.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters.")
                .Must(BeValidTitle).WithMessage("Title cannot contain prohibited words or special characters.")
                .MustAsync((command,title, cancellationToken) => BeUniqueLiveStreamTitle(command, cancellationToken)).WithMessage("The specified live stream title already exists.");

            RuleFor(x => x.StreamUrl)
                .NotEmpty().WithMessage("Stream URL is required.")
                .Must(BeValidUrl).WithMessage("Stream URL must start with http or https.");

            RuleFor(x => x.StartedAt)
                .NotEmpty().WithMessage("Start time is required.")
                .GreaterThan(DateTime.UtcNow).WithMessage("Start time must be in the future.");

            RuleFor(x => x.EndedAt)
                .NotEmpty().WithMessage("End time is required.")
                .GreaterThan(x => x.StartedAt).WithMessage("End time must be after the start time.")
                .GreaterThan(x => x.StartedAt.AddHours(2)).WithMessage("Live stream duration must be at least 2 hours.")
                .WithMessage("End time must be at least 2 hours after the start time.");
        }

        private async Task<bool> UserExists(long id, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(x => x.Id == id, cancellationToken);
        }

        private async Task<bool> BeUniqueLiveStreamTitle(CreateLiveStreamCommand command, CancellationToken cancellationToken)
        {
            return !await _context.LiveStreams
                .AnyAsync(x => x.Title.ToLower() == command.Title.ToLower() && x.HostId == command.HostId, cancellationToken);
        }

        private bool BeValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) 
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        private bool BeValidTitle(string title)
        {
            var prohibitedWords = new[] { "spam", "prohibited", "banned" }; // Örnek yasaklı kelimeler
            return !prohibitedWords.Any(word => title.ToLower().Contains(word));
        }
    }
}