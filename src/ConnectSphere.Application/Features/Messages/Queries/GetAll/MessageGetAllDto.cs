using ConnectSphere.Domain.Entities;

namespace ConnectSphere.Application.Features.Messages.Queries.GetAll;

public sealed record MessageGetAllDto
{
    public required long Id { get; set; }
    public required long SenderId { get; set; }
    public required long ReceiverId { get; set; }
    public  string Content { get; set; }
    public  DateTime SentAt { get; set; }
    public  bool IsRead { get; set; }

    public static MessageGetAllDto Create(long id, long senderId, long receiverId, string content, DateTime sentAt, bool isRead)
    {
        return new MessageGetAllDto
        {
            Id = id,
            SenderId = senderId,
            ReceiverId = receiverId,
            Content = content,
            SentAt = sentAt,
            IsRead = isRead
        };
    }

    public MessageGetAllDto(long id, long senderId, long receiverId, string content, DateTime sentAt, bool isRead)
    {
        Id = id;
        SenderId = senderId;
        ReceiverId = receiverId;
        Content = content;
        SentAt = sentAt;
        IsRead = isRead;
    }

    public MessageGetAllDto() { }
}