using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.DomainEvents;
using TSID.Creator.NET;

namespace ConnectSphere.Domain.Entities;

public sealed class Notification : EntityBase<long>
{
    public long UserId { get; set; } // Bildirimi alan kullanıcı ID
    public string Content { get; set; } // Bildirim içeriği
    public bool IsRead { get; set; } // Okundu durumu
    public DateTime SentAt { get; set; } // Gönderim tarihi
    public string NotificationType { get; set; } // Bildirim türü

    // Navigations
    public User User { get; set; }

    public static Notification Create(long userId, string content, string notificationType) // Bildirim oluşturma
    {
        if (userId <= 0) throw new ArgumentException("User ID must be a positive number.", nameof(userId));
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Content cannot be empty.", nameof(content));

        var notification = new Notification()
        {
            Id = TsidCreator.GetTsid().ToLong(),
            UserId = userId,
            Content = content,
            SentAt = DateTime.UtcNow,
            IsRead = false,
            NotificationType = notificationType // Bildirim türü
        };
        
        notification.RaiseDomainEvent(new NotificationCreatedDomainEvent(notification));
        return notification;
    }

    public void MarkAsRead() // Bildirimi okundu olarak işaretleme
    {
        IsRead = true;
    }

    public void Delete() // Bildirimi silme
    {
        RaiseDomainEvent(new NotificationDeletedDomainEvent(this));
    }

    public void SendToUser(User user) // Kullanıcıya bildirim gönderme
    {
        // Bildirim gönderme işlemleri burada yapılabilir
    }
}
    
 