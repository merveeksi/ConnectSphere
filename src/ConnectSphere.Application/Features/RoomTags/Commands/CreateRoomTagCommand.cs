using MediatR;

namespace ConnectSphere.Application.Features.RoomTags.Commands
{
    public sealed record CreateRoomTagCommand(long RoomId, string Tag) : IRequest<long>;
} 