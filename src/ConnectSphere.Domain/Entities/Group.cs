using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.DomainEvents;
using TSID.Creator.NET;

namespace ConnectSphere.Domain.Entities;

//sealed: Bu sınıftan kalıtım alınamaz, türetemezsin.
public sealed class Group : EntityBase<long>
{
    public string GroupName { get; set; } // Grup adı
    public DateTime CreatedAt { get; set; } // Grup oluşturma tarihi

    // Navigations
    public User CreatedBy { get; set; }
    public ICollection<User> Members { get; set; } // Grup üyeleri
    public ICollection<Message> Messages { get; set; } // Grup mesajları


    public Group()
    {
        Id = TsidCreator.GetTsid().ToLong();
    }
    
    public static Group Create(string groupName, User createdBy) // Grup oluşturma 
    {
        if (string.IsNullOrWhiteSpace(groupName)) throw new ArgumentException("Group name cannot be empty.", nameof(groupName));
        if (createdBy == null) throw new ArgumentNullException(nameof(createdBy), "Created by user cannot be null.");

        var group = new Group
        {
            GroupName = groupName,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = createdBy
        };
        
        group.RaiseDomainEvent(new GroupCreatedDomainEvent(group));

        return group;
    }
    
    private void Handle(GroupCreatedDomainEvent domainEvent) // Grup oluşturulduğunda yapılacak işlemler
    {
        // Domain event ile ilgili bir şeyler yapın
    }
    
    public void AddMember(User user) // Gruba üye ekleme 
    {
        if (user == null) throw new ArgumentNullException(nameof(user), "User cannot be null.");
        if (Members == null) Members = new List<User>();

        Members.Add(user);
    }
    
    public void RemoveMember(User user) // Grubu üyeden çıkarma
    {
        if (user == null) throw new ArgumentNullException(nameof(user), "User cannot be null.");
        if (Members == null) return;

        Members.Remove(user);
    }
    
    public void AddMessage(Message message) // Gruba mesaj ekleme
    {
        if (message == null) throw new ArgumentNullException(nameof(message), "Message cannot be null.");
        if (Messages == null) Messages = new List<Message>();

        Messages.Add(message);
    }
    
    public void RemoveMessage(Message message) // Grubun mesajını silme
    {
        if (message == null) throw new ArgumentNullException(nameof(message), "Message cannot be null.");
        if (Messages == null) return;

        Messages.Remove(message);
    }
    
    public void ClearMessages() // Grubun mesajlarını temizleme
    {
        if (Messages == null) return;

        Messages.Clear();
    }
    
    public void UpdateGroupName(string groupName) // Grup adını güncelleme
    {
        if (string.IsNullOrWhiteSpace(groupName)) throw new ArgumentException("Group name cannot be empty.", nameof(groupName));

        GroupName = groupName;
    }
    
    
    
}