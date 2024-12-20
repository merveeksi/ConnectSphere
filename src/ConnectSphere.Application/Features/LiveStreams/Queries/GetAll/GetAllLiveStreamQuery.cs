using ConnectSphere.Application.Common.Attributes;
using MediatR;

namespace ConnectSphere.Application.Features.LiveStreams.Queries.GetAll;

[CacheOptions(absoluteExpirationMinutes: 60, slidingExpirationMinutes: 15)]
public sealed record GetAllLiveStreamQuery : IRequest<List<LiveStreamGetAllDto>>
{
    [CacheKeyPart]
    public long HostId { get; set; }

    [CacheKeyPart]
    public string? Title { get; set; }

    [CacheKeyPart]
    public string? StreamUrl { get; set; }

    [CacheKeyPart]
    public DateTime? StartedAt { get; set; }

    [CacheKeyPart]
    public DateTime? EndedAt { get; set; }

    [CacheKeyPart]
    public bool IsActive { get; set; }

    public string CacheGroup => "LiveStreams";
    public GetAllLiveStreamQuery(long hostId, string? title, string? streamUrl, DateTime? startedAt, DateTime? endedAt, bool isActive)
    {
        HostId = hostId;
        Title = title;
        StreamUrl = streamUrl;
        StartedAt = startedAt;
        EndedAt = endedAt;
        IsActive = isActive;
    }
}