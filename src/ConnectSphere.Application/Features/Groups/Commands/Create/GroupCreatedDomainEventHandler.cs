using ConnectSphere.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ConnectSphere.Application.Features.Groups.Commands.Create;

    public sealed class GroupCreatedDomainEventHandler : INotificationHandler<GroupCreatedDomainEvent>
    {
        private readonly ILogger<GroupCreatedDomainEventHandler> _logger;

        public GroupCreatedDomainEventHandler(ILogger<GroupCreatedDomainEventHandler> logger)
        {
            _logger = logger;
        }
        
        public Task Handle(GroupCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
           _logger.LogInformation("Group created with id: {GroupId}", notification.GroupId);
              return Task.CompletedTask;
        }
    }