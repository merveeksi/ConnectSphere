using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.DomainEvents;
using TSID.Creator.NET;

namespace ConnectSphere.Domain.Entities;

public sealed class Message : EntityBase<long>
{
    public long SenderId { get; set; } // Gönderen kullanıcı ID
    public long? ReceiverId { get; set; } // Alıcı kullanıcı ID (null ise grup mesajıdır)
    public string Content { get; set; } // Mesaj içeriği
    public DateTime SentAt { get; set; } // Gönderilme tarihi
    public bool IsRead { get; set; } // Mesaj okunma durumu

    // Navigations
    public User Sender { get; set; }
    public User Receiver { get; set; }
    public long? GroupId { get; set; } // Grup ID (null ise birebir mesajdır)
    public Group Group { get; set; }

    public static Message Create(long senderId, long? receiverId, string content)
    {
        ValidateContent(content);

        var message = new Message
        {
            Id = TsidCreator.GetTsid().ToLong(),
            SenderId = senderId,
            ReceiverId = receiverId,
            Content = content,
            SentAt = DateTime.UtcNow,
            IsRead = false
        };
        
        message.RaiseDomainEvent(new MessageSentDomainEvent(message));
        
        return message;
    }

    public void MarkAsRead() // Mesajı okundu olarak işaretleme
    {
        IsRead = true;
    }
    
    public void Create() // Mesajı oluşturma
    {
        RaiseDomainEvent(new MessageCreatedDomainEvent(this));
    }

    public void Delete() // Mesajı silme
    {
        RaiseDomainEvent(new MessageDeletedDomainEvent(this));
    }

    private static void ValidateContent(string content) // Mesaj içeriğini doğrulama
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Message content cannot be empty.", nameof(content));
    }
}
