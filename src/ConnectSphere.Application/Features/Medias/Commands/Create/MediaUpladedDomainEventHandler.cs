using ConnectSphere.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ConnectSphere.Application.Features.Medias.Commands.Create;

public sealed class MediaUpladedDomainEventHandler : INotificationHandler<MediaUploadedDomainEvent>
{
    private readonly ILogger<MediaUpladedDomainEventHandler> _logger;

    public MediaUpladedDomainEventHandler(ILogger<MediaUpladedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(MediaUploadedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Media uploaded with id: {MediaId}", notification.MediaId);
        return Task.CompletedTask;
    }
}