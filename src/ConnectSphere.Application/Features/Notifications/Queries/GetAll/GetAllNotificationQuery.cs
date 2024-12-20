using ConnectSphere.Application.Common.Attributes;
using MediatR;

namespace ConnectSphere.Application.Features.Notifications.Queries.GetAll;

[CacheOptions(absoluteExpirationMinutes: 60, slidingExpirationMinutes: 15)]
public sealed record GetAllNotificationQuery : IRequest<List<NotificationGetAllDto>>
{
    [CacheKeyPart]
    public long UserId { get; set; }

    [CacheKeyPart]
    public string? Content { get; set; }

    [CacheKeyPart]
    public string? NotificationType { get; set; }

    [CacheKeyPart]
    public bool IsRead { get; set; }

    [CacheKeyPart]
    public DateTime? SentAt { get; set; }

    public string CacheGroup => "Notifications";
    public GetAllNotificationQuery(long userId, string? content, string? notificationType, bool isRead, DateTime? sentAt)
    {
        UserId = userId;
        Content = content;
        NotificationType = notificationType;
        IsRead = isRead;
        SentAt = sentAt;
    }
}