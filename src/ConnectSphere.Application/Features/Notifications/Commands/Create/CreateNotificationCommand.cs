using ConnectSphere.Domain.ValueObjects;
using MediatR;

namespace ConnectSphere.Application.Features.Notifications.Commands.Create;

public sealed record CreateNotificationCommand(long UserId, Content Content, string NotificationType, DateTime SentAt) : IRequest<long>;