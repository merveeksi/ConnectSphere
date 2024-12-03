namespace ConnectSphere.Domain.DomainEvents;

public record MessageCreatedDomainEvent(long MessageId, string messageContent) : IDomainEvent;