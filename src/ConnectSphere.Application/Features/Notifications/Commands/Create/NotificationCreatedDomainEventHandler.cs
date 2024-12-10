using ConnectSphere.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ConnectSphere.Application.Features.Notifications.Commands.Create;

public sealed class NotificationCreatedDomainEventHandler : INotificationHandler<NotificationCreatedDomainEvent>
{
    private readonly ILogger<NotificationCreatedDomainEventHandler> _logger;
    
    public NotificationCreatedDomainEventHandler(ILogger<NotificationCreatedDomainEventHandler> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(NotificationCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Notification created with id: {NotificationId}", notification.NotificationId);
        return Task.CompletedTask;
    }
}