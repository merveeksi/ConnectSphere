using ConnectSphere.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ConnectSphere.Application.Features.Messages.Commands.Create;

public sealed class MessageSentDomainEventHandler : INotificationHandler<MessageSentDomainEvent>
{
    private readonly ILogger<MessageSentDomainEventHandler> _logger;
    
    public MessageSentDomainEventHandler(ILogger<MessageSentDomainEventHandler> logger)
    {
        _logger = logger;
    }
    
    public Task Handle(MessageSentDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Message sent with id: {MessageId}", notification.MessageId);
        return Task.CompletedTask;
    }
}