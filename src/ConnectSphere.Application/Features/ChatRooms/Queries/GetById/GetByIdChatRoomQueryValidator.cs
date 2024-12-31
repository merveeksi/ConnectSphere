using FluentValidation;

namespace ConnectSphere.Application.Features.ChatRooms.Queries.GetById
{
    public sealed class GetByIdChatRoomQueryValidator : AbstractValidator<GetByIdChatRoomQuery>
    {
        public GetByIdChatRoomQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");
        }
    }
} 