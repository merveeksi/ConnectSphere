using ConnectSphere.Domain.Common.Events;
using ConnectSphere.Domain.DomainEvents;

namespace ConnectSphere.Domain.Common.Entities
{
    // abstract newlememek için kullanılır. Yani bu sınıftan nesne oluşturulamaz.
    public abstract class EntityBase<TKey> : IEntity<TKey>, ICreatedByEntity, IModifiedByEntity where TKey : struct
    {
        //virtual: override edilebilir demek
    public virtual TKey Id { get; set; }
    public virtual string CreatedByUserId { get; set; }
    public virtual DateTimeOffset CreatedOn { get; set; }
    
    public virtual string? ModifiedByUserId { get; set; }
    public virtual DateTimeOffset? ModifiedOn { get; set; }
    
    
    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyList<IDomainEvent> GetDomainEvents() => _domainEvents.AsReadOnly();
    public void RaiseDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
    }
   
}