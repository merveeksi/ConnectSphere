using ConnectSphere.Domain.Common.Entities;

namespace ConnectSphere.Domain.Entities;

public sealed class Notification : EntityBase<long>
{
    public long UserId { get; set; } // Bildirimi alan kullanıcı ID
    public string Content { get; set; } // Bildirim içeriği
    public bool IsRead { get; set; } // Okundu durumu
    public DateTime SentAt { get; set; } // Gönderim tarihi

    // Navigations
    public User User { get; set; }
}