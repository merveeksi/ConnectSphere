using ConnectSphere.Domain.Entities;

namespace ConnectSphere.Application.Features.Notifications.Queries.GetById;

public sealed record NotificationGetByIdDto
{
    public required long Id { get; set; }
    public required long UserId { get; set; }
    public  string Content { get; set; }
    public  string NotificationType { get; set; }
    public  bool IsRead { get; set; }
    public  DateTime SentAt { get; set; }

    public static NotificationGetByIdDto Create(Notification notification)
    {
        return new NotificationGetByIdDto
        {
            Id = notification.Id,
            UserId = notification.UserId,
            Content = notification.Content.Value,
            NotificationType = notification.NotificationType,
            IsRead = notification.IsRead,
            SentAt = notification.SentAt
        };
    }

    public NotificationGetByIdDto(long id, long userId, string content, string notificationType, bool isRead, DateTime sentAt)
    {
        Id = id;
        UserId = userId;
        Content = content;
        NotificationType = notificationType;
        IsRead = isRead;
        SentAt = sentAt;
    }

    public NotificationGetByIdDto()
    {
    }
}