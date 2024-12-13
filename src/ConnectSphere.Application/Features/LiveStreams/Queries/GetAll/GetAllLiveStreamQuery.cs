using MediatR;

namespace ConnectSphere.Application.Features.LiveStreams.Queries.GetAll;

public sealed record GetAllLiveStreamQuery : IRequest<List<LiveStreamGetAllDto>>
{
    public long HostId { get; set; }
    public string? Title { get; set; }
    public string? StreamUrl { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? EndedAt { get; set; }
    public bool IsActive { get; set; }
}