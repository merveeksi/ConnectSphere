using ConnectSphere.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ConnectSphere.Application.Features.Notifications.Commands.Create;

public class NotificationDeletedDomainEventHandler : INotificationHandler<NotificationDeletedDomainEvent>
{
    private readonly ILogger<NotificationDeletedDomainEventHandler> _logger;
    
    public NotificationDeletedDomainEventHandler(ILogger<NotificationDeletedDomainEventHandler> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(NotificationDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Notification deleted with id: {NotificationId}", notification.NotificationId);
        return Task.CompletedTask;
    }
}