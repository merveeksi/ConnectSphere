using ConnectSphere.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.RoomTags.Commands
{
    public class CreateRoomTagCommandValidator : AbstractValidator<CreateRoomTagCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateRoomTagCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.RoomId)
                .NotEmpty()
                .WithMessage("RoomId is required.")
                .MustAsync(RoomExists)
                .WithMessage("Room does not exist.");

            RuleFor(x => x.Tag)
                .NotEmpty()
                .WithMessage("Tag is required.");
        }

        private async Task<bool> RoomExists(long roomId, CancellationToken cancellationToken)
        {
            return await _context.ChatRooms.AnyAsync(x => x.Id == roomId, cancellationToken);
        }
    }
} 