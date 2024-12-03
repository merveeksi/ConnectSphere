namespace ConnectSphere.Domain.DomainEvents;

public record LiveStreamStartedDomainEvent(long LiveStreamId) : IDomainEvent;