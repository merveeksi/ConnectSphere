using ConnectSphere.Domain.Common.Events;
using ConnectSphere.Domain.Entities;

namespace ConnectSphere.Domain.DomainEvents;

public record NotificationCreatedDomainEvent(Notification NotificationId) : IDomainEvent
{
    
}