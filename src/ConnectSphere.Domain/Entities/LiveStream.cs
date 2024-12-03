using ConnectSphere.Domain.Common.Entities;
using TSID.Creator.NET;

namespace ConnectSphere.Domain.Entities;

public sealed class LiveStream : EntityBase<long>
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
        var liveStream = new LiveStream
        {
            Id = TsidCreator.GetTsid().ToLong(),
            HostId = hostId,
            Title = title,
            StreamUrl = streamUrl,
            StartedAt = DateTime.UtcNow
        };
        
        liveStream.RaiseDomainEvent(new LiveStreamStartedDomainEvent(liveStream.Id, liveStream.Title));

        return liveStream;
    }
}