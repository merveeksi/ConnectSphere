using MediatR;

namespace ConnectSphere.Application.Features.Notifications.Queries.GetById;

public record GetByIdNotificationQuery(long Id) : IRequest<NotificationGetByIdDto>;