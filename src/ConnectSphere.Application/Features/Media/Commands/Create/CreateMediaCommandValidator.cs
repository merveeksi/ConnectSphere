using ConnectSphere.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Media.Commands.Create
{
    public sealed class CreateMediaCommandValidator : AbstractValidator<CreateMediaCommand>
    {
        private readonly IApplicationDbContext _context;
        private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".mp4", ".avi" }; // İzin verilen uzantılar

        public CreateMediaCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.UploadedById)
                .NotEmpty().WithMessage("Uploaded By ID is required.")
                .MustAsync(UserExists).WithMessage("User does not exist.");

            RuleFor(x => x.Url)
                .NotEmpty().WithMessage("URL is required.")
                .Must(BeValidUrl).WithMessage("URL must start with http or https.");

            RuleFor(x => x.MediaType)
                .NotEmpty().WithMessage("Media type is required.")
                .MaximumLength(50).WithMessage("Media type must not exceed 50 characters.")
                .Must(BeValidMediaType).WithMessage("Invalid media type.");

            RuleFor(x => x.UploadedAt)
                .NotEmpty().WithMessage("UploadedAt is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("UploadedAt cannot be in the future.");

            RuleFor(x => x.FileSize)
                .NotEmpty().WithMessage("File size is required.")
                .Must(BeValidFileSize).WithMessage($"File size cannot exceed {MaxFileSize / (1024 * 1024)} MB.");
        }

        private async Task<bool> UserExists(long id, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(x => x.Id == id, cancellationToken);
        }

        private bool BeValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) 
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        private bool BeValidMediaType(string mediaType)
        {
            return AllowedExtensions.Any(ext => mediaType.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }

        private bool BeValidFileSize(string fileSize)
        {
            if (long.TryParse(fileSize, out var size))
            {
                return size <= MaxFileSize;
            }
            return false;
        }
    }
}