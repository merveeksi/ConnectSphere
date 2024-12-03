namespace ConnectSphere.Domain.DomainEvents;

public record MediaUploadedDomainEvent(long MediaId, string mediaUrl) : IDomainEvent;