using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Entities;
using ConnectSphere.Domain.Enum;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.ChatRooms.Commands
{
    public class CreateChatRoomCommandValidator : AbstractValidator<CreateChatRoomCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateChatRoomCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(x => x.ChatRoomName)
                .NotEmpty()
                .WithMessage("ChatRoomName is required.")
                .MustAsync(CheckIfChatRoomNameExists)
                .WithMessage("ChatRoomName already exists.");
            
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.");

            RuleFor(x => x.CreatorId)
                .NotEmpty()
                .WithMessage("CreatorId is required.")
                .MustAsync(UserExists)
                .WithMessage("Creator user does not exist.");

            RuleFor(x => x.MaxParticipants)
                .NotEmpty()
                .WithMessage("MaxParticipants is required.")
                .GreaterThan(0).WithMessage("MaxParticipants must be greater than 0.");

            RuleFor(x => x.IsPrivate)
                .NotEmpty()
                .WithMessage("IsPrivate is required.")
                .Must(BeValidIsPrivate).WithMessage("Invalid IsPrivate value.");

            RuleFor(x => x.Status)
                .NotEmpty()
                .WithMessage("Status is required.")
                .Must(BeValidStatus).WithMessage("Invalid Status value.");

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .WithMessage("CategoryId is required.")
                .MustAsync(CategoryExists)
                .WithMessage("Category does not exist.");
        }

        private async Task<bool> CheckIfChatRoomNameExists(string chatRoomName, CancellationToken cancellationToken)
        {
            return !await _context.ChatRooms.AnyAsync(x => x.ChatRoomName == chatRoomName, cancellationToken);
        }

        private async Task<bool> UserExists(long creatorId, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(x => x.Id == creatorId, cancellationToken);
        }

        private bool BeValidIsPrivate(bool isPrivate)
        {
            return isPrivate == true || isPrivate == false;
        }

        private bool BeValidStatus(RoomStatus status)
        {
            return status == RoomStatus.Active || status == RoomStatus.Inactive;
        }

        private async Task<bool> CategoryExists(long categoryId, CancellationToken cancellationToken)
        {
            return await _context.RoomCategories.AnyAsync(x => x.Id == categoryId, cancellationToken);
        }
    }
}