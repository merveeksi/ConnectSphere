using ConnectSphere.Domain.Enum;
using MediatR;

namespace ConnectSphere.Application.Features.RoomParticipants.Commands
{
    public sealed record CreateRoomParticipantCommand(long RoomId, long UserId, RoomRole Role) : IRequest<long>;
} 