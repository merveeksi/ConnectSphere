using MediatR;

namespace ConnectSphere.Application.Features.Notifications.Queries.GetAll;

public sealed record GetAllNotificationQuery : IRequest<List<NotificationGetAllDto>>
{
    public long UserId { get; set; }
    public string? Content { get; set; }
    public string? NotificationType { get; set; }
    public bool IsRead { get; set; }
    public DateTime? SentAt { get; set; }
}