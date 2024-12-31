using FluentValidation;

namespace ConnectSphere.Application.Features.ChatRooms.Queries.GetAll
{
    public sealed class GetAllChatRoomsQueryValidator : AbstractValidator<GetAllChatRoomsQuery>
    {
        public GetAllChatRoomsQueryValidator()
        {
            // Burada herhangi bir özel doğrulama yoksa, validator boş kalabilir.
        }
    }
} 