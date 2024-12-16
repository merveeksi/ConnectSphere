using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.DomainEvents;
using ConnectSphere.Domain.ValueObjects;
using TSID.Creator.NET;

namespace ConnectSphere.Domain.Entities;

//sealed: Bu sınıftan kalıtım alınamaz, türetemezsin.
public sealed class Group : EntityBase<long>
{
    public GroupName GroupName { get; private set; } // private set yapıldı
    public DateTime CreatedAt { get; private set; }
    public long CreatedById { get; private set; } // CreatedBy için ID eklendi

    // Navigations
    public User CreatedBy { get; private set; }
    public ICollection<User> Members { get; private set; } = [];
    public ICollection<Message> Messages { get; private set; } = [];

    private Group() // private constructor
    {
        Id = TsidCreator.GetTsid().ToLong();
    }
    
    public static Group Create(GroupName groupName, long createdBy)
    {
        if (groupName == null) 
            throw new ArgumentNullException(nameof(groupName));
        if (createdBy <= 0) 
            throw new ArgumentNullException(nameof(createdBy));

        var group = new Group
        {
            Id = TsidCreator.GetTsid().ToLong(),
            GroupName = groupName,
            CreatedAt = DateTime.UtcNow,
            CreatedById = createdBy
        };
        
        group.RaiseDomainEvent(new GroupCreatedDomainEvent(group.Id));
        return group;
    }

    public void UpdateGroupName(GroupName newGroupName)
    {
        if (newGroupName == null)
            throw new ArgumentNullException(nameof(newGroupName));

        GroupName = newGroupName;
    }

    public void AddMember(User user)
    {
        if (user == null) 
            throw new ArgumentNullException(nameof(user));

        if (Members.Any(m => m.Id == user.Id))
            throw new InvalidOperationException("User is already a member of this group.");

        Members.Add(user);
    }
    
    public void RemoveMember(User user)
    {
        if (user == null) 
            throw new ArgumentNullException(nameof(user));

        var member = Members.FirstOrDefault(m => m.Id == user.Id);
        if (member == null)
            throw new InvalidOperationException("User is not a member of this group.");

        Members.Remove(member);
    }
    
    public void AddMessage(Message message)
    {
        if (message == null) 
            throw new ArgumentNullException(nameof(message));

        Messages.Add(message);
    }
    
    public void RemoveMessage(Message message)
    {
        if (message == null) 
            throw new ArgumentNullException(nameof(message));

        var existingMessage = Messages.FirstOrDefault(m => m.Id == message.Id);
        if (existingMessage == null)
            throw new InvalidOperationException("Message not found in this group.");

        Messages.Remove(existingMessage);
    }
    
    public void ClearMessages()
    {
        Messages.Clear();
    }

    public string GetGroupInitials()
    {
        return GroupName.GetGroupInitials();
    }

    public bool IsDefaultGroup()
    {
        return GroupName.IsDefaultGroup();
    }
}