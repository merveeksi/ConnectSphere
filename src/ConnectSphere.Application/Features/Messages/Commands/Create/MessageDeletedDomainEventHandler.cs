using ConnectSphere.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ConnectSphere.Application.Features.Messages.Commands.Create;

public sealed class MessageDeletedDomainEventHandler : INotificationHandler<MessageDeletedDomainEvent>
{
    private readonly ILogger<MessageDeletedDomainEventHandler> _logger;
    
    public MessageDeletedDomainEventHandler(ILogger<MessageDeletedDomainEventHandler> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(MessageDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}