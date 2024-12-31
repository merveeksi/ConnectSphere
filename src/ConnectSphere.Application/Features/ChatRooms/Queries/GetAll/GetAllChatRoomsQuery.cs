using MediatR;

namespace ConnectSphere.Application.Features.ChatRooms.Queries.GetAll
{
    public sealed record GetAllChatRoomsQuery : IRequest<List<ChatRoomGetAllDto>>, ICacheable
    {
        public string CacheGroup => "ChatRooms";
    }
} 