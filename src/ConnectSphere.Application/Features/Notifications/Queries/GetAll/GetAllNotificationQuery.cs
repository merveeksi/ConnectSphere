using MediatR;

namespace ConnectSphere.Application.Features.Notifications.Queries.GetAll;

public sealed record GetAllNotificationQuery(long UserId) : IRequest<List<NotificationGetAllDto>>;