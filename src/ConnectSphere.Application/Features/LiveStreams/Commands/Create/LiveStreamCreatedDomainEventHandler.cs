using ConnectSphere.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ConnectSphere.Application.Features.LiveStreams.Commands.Create;

public sealed class LiveStreamCreatedDomainEventHandler : INotificationHandler<LiveStreamStartedDomainEvent>
{
    private readonly ILogger<LiveStreamCreatedDomainEventHandler> _logger;

    public LiveStreamCreatedDomainEventHandler(ILogger<LiveStreamCreatedDomainEventHandler> logger)
    {
        _logger = logger;
    }
    

    public Task Handle(LiveStreamStartedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Live stream started with id: {LiveStreamId}", notification.LiveStreamId);
        return Task.CompletedTask;
    }
} 