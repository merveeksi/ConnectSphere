using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.Enum;

namespace ConnectSphere.Domain.Entities;

public sealed class RoomParticipant : EntityBase<long>
{
    public RoomParticipant(ChatRoom chatRoom)
    {
        ChatRoom = chatRoom;
    }

    private RoomParticipant(long roomId, long userId, RoomRole role)
    {
        RoomId = roomId;
        UserId = userId;
        Role = role;
    }

    public long RoomId { get; private set; }
    public long UserId { get; private set; }
    public RoomRole Role { get; private set; }
    public ChatRoom ChatRoom { get; private set; }

    public static RoomParticipant Create(long roomId, long userId, RoomRole role)
    {
        return new RoomParticipant(roomId, userId, role);
    }
}