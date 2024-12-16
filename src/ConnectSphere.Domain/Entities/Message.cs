using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.DomainEvents;
using ConnectSphere.Domain.ValueObjects;
using TSID.Creator.NET;

namespace ConnectSphere.Domain.Entities;

public sealed class Message : EntityBase<long>
{
    public long SenderId { get; private set; } // Gönderen kullanıcı ID
    public long ReceiverId { get; private set; } // Alıcı kullanıcı ID (null ise grup mesajıdır)
    public Content Content { get; private set; } // Mesaj içeriği artık Content value object
    public DateTime SentAt { get; private set; } // Gönderilme tarihi
    public bool IsRead { get; private set; } = false; // Mesaj okunma durumu, varsayılan false

    // Navigations
    public User Sender { get; private set; }
    public User Receiver { get; private set; }
    public long? GroupId { get; private set; } // Grup ID (null ise birebir mesajdır)
    public Group Group { get; private set; }

    public static Message Create(long senderId, long receiverId, Content content)
    {
        var message = new Message
        {
            Id = TsidCreator.GetTsid().ToLong(),
            SenderId = senderId,
            ReceiverId = receiverId,
            Content = Content.Create(content), // Content value object oluşturuluyor
            SentAt = DateTime.UtcNow
        };
        
        message.RaiseDomainEvent(new MessageSentDomainEvent(message));
        
        return message;
    }

    public void MarkAsRead() // Mesajı okundu olarak işaretleme
    {
        IsRead = true;
    }

    public void Delete() // Mesajı silme
    {
        RaiseDomainEvent(new MessageDeletedDomainEvent(this));
    }

    public string GetMessagePreview() // Mesaj önizlemesi alma
    {
        return Content.GetPreview();
    }

    public bool ContainsUrl() // Mesajda URL var mı kontrolü
    {
        return Content.ContainsUrl;
    }
}
