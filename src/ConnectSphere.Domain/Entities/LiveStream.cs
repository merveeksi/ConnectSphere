using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.DomainEvents;
using TSID.Creator.NET;

namespace ConnectSphere.Domain.Entities;

public sealed class LiveStream : EntityBase<long>
{
    public long HostId { get; private set; } // Yayını başlatan kullanıcı ID
    public string Title { get; private set; } // Yayın başlığı
    public string StreamUrl { get; private set; } // Yayın URL'si
    public DateTime StartedAt { get; private set; } // Yayın başlangıç tarihi
    public DateTime? EndedAt { get; private set; } // Yayın bitiş tarihi
    public bool IsActive => EndedAt == null; // Yayın devam ediyor mu?

    // Navigations
    public User Host { get; set; }
    
    public List<string> Messages { get; private set; } = new List<string>(); // Yayın sırasında gönderilen mesajlar
    public HashSet<long> MutedUserIds { get; private set; } = new HashSet<long>(); // Susturulan kullanıcı ID'leri

    public static LiveStream Create(long hostId, string title, string streamUrl) // Yayın oluşturma
    {
        if (hostId <= 0) throw new ArgumentException("Host ID must be a positive number.", nameof(hostId));
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be empty.", nameof(title));
        if (string.IsNullOrWhiteSpace(streamUrl)) throw new ArgumentException("Stream URL cannot be empty.", nameof(streamUrl));

        var liveStream = new LiveStream()
        {
            Id = TsidCreator.GetTsid().ToLong(),
            HostId = hostId,
            Title = title,
            StreamUrl = streamUrl,
            StartedAt = DateTime.UtcNow
        };
        
        liveStream.RaiseDomainEvent(new LiveStreamStartedDomainEvent(liveStream));

        return liveStream;
    }
    
    public void End() // Yayını sonlandırma
    {
        if (EndedAt.HasValue) throw new InvalidOperationException("Live stream has already ended.");

        EndedAt = DateTime.UtcNow;
    }
    
    public void SendMessage(string message) // Mesaj gönderme
    {
        if (string.IsNullOrWhiteSpace(message)) throw new ArgumentException("Message cannot be empty.", nameof(message));
        if (Messages.Count >= 1000) throw new InvalidOperationException("Maximum message limit reached.");
        Messages.Add(message);
    }

    public void MuteUser(long userId) // Kullanıcıyı susturma
    {
        if (userId <= 0) throw new ArgumentException("User ID must be positive.", nameof(userId));
        if (MutedUserIds.Contains(userId)) throw new InvalidOperationException("User is already muted.");
        MutedUserIds.Add(userId);
    }

    public void UnmuteUser(long userId) // Susturmayı kaldırma
    {
        if (userId <= 0) throw new ArgumentException("User ID must be positive.", nameof(userId));
        if (!MutedUserIds.Contains(userId)) throw new InvalidOperationException("User is not muted.");
        MutedUserIds.Remove(userId);
    }
}