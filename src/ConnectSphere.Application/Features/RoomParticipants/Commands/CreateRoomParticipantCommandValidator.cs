using ConnectSphere.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.RoomParticipants.Commands
{
    public class CreateRoomParticipantCommandValidator : AbstractValidator<CreateRoomParticipantCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateRoomParticipantCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.RoomId)
                .NotEmpty()
                .WithMessage("RoomId is required.")
                .MustAsync(RoomExists)
                .WithMessage("Room does not exist.");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId is required.")
                .MustAsync(UserExists)
                .WithMessage("User does not exist.");

            RuleFor(x => x.Role)
                .IsInEnum()
                .WithMessage("Invalid RoomRole value.");
        }

        private async Task<bool> RoomExists(long roomId, CancellationToken cancellationToken)
        {
            return await _context.ChatRooms.AnyAsync(x => x.Id == roomId, cancellationToken);
        }

        private async Task<bool> UserExists(long userId, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(x => x.Id == userId, cancellationToken);
        }
    }
} 