using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.Enum;
using TSID.Creator.NET;

namespace ConnectSphere.Domain.Entities;

public sealed class ChatRoom : EntityBase<long>
{
    public string ChatRoomName { get; private set; }
    public string Description { get; private set; }
    public long CreatorId { get; private set; }  
    public int MaxParticipants { get; private set; }
    public bool IsPrivate { get; private set; }
    public RoomStatus Status { get; private set; }
    public long CategoryId { get; private set; }
    public List<RoomParticipant> Participants { get; private set; }
    public List<RoomTag> Tags { get; private set; }

    private ChatRoom() { }

    public static ChatRoom Create(
        string chatRoomName, 
        string description,
        long creatorId,
        int maxParticipants,
        bool isPrivate,
        RoomStatus status,
        long categoryId)
    {
        return new ChatRoom
        {
            Id = TsidCreator.GetTsid().ToLong(),
            ChatRoomName = chatRoomName,
            Description = description,
            CreatorId = creatorId,
            MaxParticipants = maxParticipants,
            IsPrivate = isPrivate,
            Status = status,
            CategoryId = categoryId,
            Participants = new List<RoomParticipant>(),
            Tags = new List<RoomTag>()
        };
    }
}   