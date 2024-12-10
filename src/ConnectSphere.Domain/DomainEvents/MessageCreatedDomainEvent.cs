using ConnectSphere.Domain.Common.Events;
using ConnectSphere.Domain.Entities;

namespace ConnectSphere.Domain.DomainEvents;

public record MessageCreatedDomainEvent(long MessageId) : IDomainEvent;