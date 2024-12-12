using ConnectSphere.Domain.Entities;

namespace ConnectSphere.Application.Features.Notifications.Queries.GetAll;

public sealed record NotificationGetAllDto
{
    public required long Id { get; set; }
    public required long UserId { get; set; }
    public  string Content { get; set; }
    public  string NotificationType { get; set; }
    public  bool IsRead { get; set; }
    public  DateTime SentAt { get; set; }

    public static NotificationGetAllDto Create(Notification notification)
    {
        return new NotificationGetAllDto
        {
            Id = notification.Id,
            UserId = notification.UserId,
            Content = notification.Content.Value,
            NotificationType = notification.NotificationType,
            IsRead = notification.IsRead,
            SentAt = notification.SentAt
        };
    }

    public NotificationGetAllDto(long id, long userId, string content, string notificationType, bool isRead, DateTime sentAt)
    {
        Id = id;
        UserId = userId;
        Content = content;
        NotificationType = notificationType;
        IsRead = isRead;
        SentAt = sentAt;
    }

    public NotificationGetAllDto() { }
}