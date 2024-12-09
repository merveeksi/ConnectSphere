using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.Enums;

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
}