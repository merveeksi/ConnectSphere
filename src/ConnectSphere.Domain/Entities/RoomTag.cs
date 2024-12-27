using ConnectSphere.Domain.Common.Entities;

namespace ConnectSphere.Domain.Entities;

public sealed class RoomTag : EntityBase<long>
{
    public RoomTag(long roomId, string tag, ChatRoom chatRoom)
    {
        RoomId = roomId;
        Tag = tag;
        ChatRoom = chatRoom;
    }

    public long RoomId { get; private set; }
    public string Tag { get; private set; }
    public ChatRoom ChatRoom { get; private set; }
}