using MediatR;

namespace ConnectSphere.Application.Features.LiveStreams.Queries.GetAll;

public sealed record GetAllLiveStreamQuery(long HostId) : IRequest<List<LiveStreamGetAllDto>>;