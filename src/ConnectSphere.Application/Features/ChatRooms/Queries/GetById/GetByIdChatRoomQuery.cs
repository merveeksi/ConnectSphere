using MediatR;

namespace ConnectSphere.Application.Features.ChatRooms.Queries.GetById
{
    public record GetByIdChatRoomQuery(long Id) : IRequest<ChatRoomGetByIdDto>;
} 