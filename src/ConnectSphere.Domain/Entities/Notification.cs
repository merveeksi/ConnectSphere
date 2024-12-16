using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.DomainEvents;
using ConnectSphere.Domain.ValueObjects;
using TSID.Creator.NET;

namespace ConnectSphere.Domain.Entities;

public sealed class Notification : EntityBase<long>
{
    public long UserId { get; private set; } // Bildirimi alan kullanıcı ID
    public Content Content { get; private set; } // Bildirim içeriği artık Content value object
    public bool IsRead { get; private set; } = false; // Okundu durumu, varsayılan false
    public DateTime SentAt { get; private set; } // Gönderim tarihi
    public string NotificationType { get; private set; } // Bildirim türü

    // Navigations
    public User User { get; private set; }

    public static Notification Create(long userId, Content content, string notificationType)
    {
        if (userId <= 0) 
            throw new ArgumentException("User ID must be a positive number.", nameof(userId));

        var notification = new Notification()
        {
            Id = TsidCreator.GetTsid().ToLong(),
            UserId = userId,
            Content = Content.Create(content), // Content value object oluşturuluyor
            SentAt = DateTime.UtcNow,
            IsRead = false,
            NotificationType = notificationType
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

    public string GetNotificationPreview() // Bildirim önizlemesi alma
    {
        return Content.GetPreview();
    }

    public bool ContainsKeyword(string keyword) // Bildirimde anahtar kelime var mı kontrolü
    {
        return Content.ContainsKeyword(keyword);
    }
}
    
 