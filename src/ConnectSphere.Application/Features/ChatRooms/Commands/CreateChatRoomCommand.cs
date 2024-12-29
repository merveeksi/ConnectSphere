using ConnectSphere.Domain.Entities;
using ConnectSphere.Domain.Enum;
using MediatR;

namespace ConnectSphere.Application.Features.ChatRooms.Commands
{
    public sealed record CreateChatRoomCommand(string ChatRoomName, string Description, long CreatorId, int MaxParticipants, 
    bool IsPrivate, RoomStatus Status, long CategoryId) : IRequest<long>;
}