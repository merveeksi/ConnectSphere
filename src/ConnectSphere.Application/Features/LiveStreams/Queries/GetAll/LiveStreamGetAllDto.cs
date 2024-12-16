using ConnectSphere.Domain.Entities;

namespace ConnectSphere.Application.Features.LiveStreams.Queries.GetAll;

public sealed record LiveStreamGetAllDto
{
    public required long Id { get; set; }
    public required long HostId { get; set; }
    public  string Title { get; set; }
    public  string StreamUrl { get; set; }
    public  DateTime StartedAt { get; set; }
    public  DateTime? EndedAt { get; set; }
    public  bool IsActive { get; set; }

    public static LiveStreamGetAllDto Create(long id, long hostId, string title, string streamUrl, 
        DateTime startedAt, DateTime? endedAt, bool isActive)
    {
        return new LiveStreamGetAllDto
        {
            Id = id,
            HostId = hostId,
            Title = title,
            StreamUrl = streamUrl,
            StartedAt = startedAt,
            EndedAt = endedAt,
            IsActive = isActive
        };
    }

    public LiveStreamGetAllDto(long id, long hostId, string title, string streamUrl, 
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

    public LiveStreamGetAllDto() { }
}