using ConnectSphere.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ConnectSphere.Application.Features.Medias.Commands.Create;

public sealed class MediaDeletedDomainEventHandler : INotificationHandler<MediaDeletedDomainEvent>
{
    private readonly ILogger<MediaDeletedDomainEventHandler> _logger;

    public MediaDeletedDomainEventHandler(ILogger<MediaDeletedDomainEventHandler> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(MediaDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Media deleted with id: {MediaId}", notification.MediaId);
        return Task.CompletedTask;
    }
}