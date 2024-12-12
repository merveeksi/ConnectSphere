using ConnectSphere.Domain.Entities;

namespace ConnectSphere.Application.Features.Messages.Queries;

public sealed record MessageGetByIdDto
{
    public required long Id { get; set; }
    public required long SenderId { get; set; }
    public required long ReceiverId { get; set; }
    public  string Content { get; set; }
    public  DateTime SentAt { get; set; }
    public  bool IsRead { get; set; }
   

    public static MessageGetByIdDto Create(Message message)
    {
        return new MessageGetByIdDto
        {
            Id = message.Id,
            SenderId = message.SenderId,
            ReceiverId = message.ReceiverId,
            Content = message.Content.Value,
            SentAt = message.SentAt,
            IsRead = message.IsRead
        };
    }

    public MessageGetByIdDto(long id, long senderId, long receiverId, string content, DateTime sentAt, bool isRead)
    {
        Id = id;
        SenderId = senderId;
        ReceiverId = receiverId;
        Content = content;
        SentAt = sentAt;
        IsRead = isRead;
    }

    public MessageGetByIdDto()
    {
    }
}