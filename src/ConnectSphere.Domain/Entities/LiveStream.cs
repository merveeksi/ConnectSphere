using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.DomainEvents;
using TSID.Creator.NET;

namespace ConnectSphere.Domain.Entities;

public sealed class LiveStream
{
    public long HostId { get; set; } // Yayını başlatan kullanıcı ID
    public string Title { get; set; } // Yayın başlığı
    public string StreamUrl { get; set; } // Yayın URL'si
    public DateTime StartedAt { get; set; } // Yayın başlangıç tarihi
    public DateTime? EndedAt { get; set; } // Yayın bitiş tarihi

    // Navigations
    public User Host { get; set; }
    
    public static LiveStream Create(long hostId, string title, string streamUrl)
    {
        if (hostId <= 0) throw new ArgumentException("Host ID must be a positive number.", nameof(hostId));
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be empty.", nameof(title));
        if (string.IsNullOrWhiteSpace(streamUrl)) throw new ArgumentException("Stream URL cannot be empty.", nameof(streamUrl));

        var liveStream = new LiveStream
        {
            Id = TsidCreator.GetTsid().ToLong(),
            HostId = hostId,
            Title = title,
            StreamUrl = streamUrl,
            StartedAt = DateTime.UtcNow
        };

        // Since LiveStream does not have an Id property, we will not set it here.
        // Instead, we will assume that the LiveStreamStartedDomainEvent constructor needs to be updated to not require an Id.
        liveStream.RaiseDomainEvent(new LiveStreamStartedDomainEvent(liveStream.Title));

        return liveStream;
    }
    
}