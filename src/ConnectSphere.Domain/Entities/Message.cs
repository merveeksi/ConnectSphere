using System.Text.RegularExpressions;
using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.DomainEvents;
using ConnectSphere.Domain.Entities;
using TSID.Creator.NET;
using Group = System.Text.RegularExpressions.Group;

namespace ConnectSphere.Domain.Enums;

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
}

public static Message Create(long senderId, long receiverId, string content)
{
    var message = new Message
    {
       Id = TsidCreator.GetTsid().ToLong(),
        SenderId = senderId,
        ReceiverId = receiverId,
        Content = content,
        SentAt = DateTime.UtcNow,
        IsRead = false
    };

    message.RaiseDomainEvent(new MediaUploadedDomainEvent(message.Id, message.Content));
    return message;
}
