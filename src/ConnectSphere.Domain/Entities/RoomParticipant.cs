using ConnectSphere.Domain.Enum;

namespace ConnectSphere.Domain.Entities;

public sealed class RoomParticipant
{
    public RoomParticipant(ChatRoom chatRoom)
    {
        ChatRoom = chatRoom;
    }

    public long RoomId { get; private set; }
    public long UserId { get; private set; }
    public RoomRole Role { get; private set; }
    public ChatRoom ChatRoom { get; private set; }
}