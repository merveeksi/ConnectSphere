using ConnectSphere.Domain.Entities;

namespace ConnectSphere.Application.Features.LiveStreams.Queries.GetById;

public sealed record LiveStreamGetByIdDto
{
    public required long Id { get; set; }
    public required long HostId { get; set; }
    public  string Title { get; set; }
    public  string StreamUrl { get; set; }
    public  DateTime StartedAt { get; set; }
    public  DateTime? EndedAt { get; set; }
    public  bool IsActive { get; set; }

    public static LiveStreamGetByIdDto Create(LiveStream liveStream)
    {
        return new LiveStreamGetByIdDto
        {
            Id = liveStream.Id,
            HostId = liveStream.HostId,
            Title = liveStream.Title,
            StreamUrl = liveStream.StreamUrl,
            StartedAt = liveStream.StartedAt,
            EndedAt = liveStream.EndedAt,
            IsActive = liveStream.IsActive
        };
    }

    public LiveStreamGetByIdDto(long id, long hostId, string title, string streamUrl, 
        DateTime startedAt, DateTime? endedAt, bool isActive)
    {
        Id = id;
        HostId = hostId;
        Title = title;
        StreamUrl = streamUrl;
        StartedAt = startedAt;
        EndedAt = endedAt;
        IsActive = isActive;
    }

    public LiveStreamGetByIdDto()
    {
    }
}