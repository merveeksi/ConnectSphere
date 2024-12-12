using MediatR;

namespace ConnectSphere.Application.Features.LiveStreams.Queries.GetById;

public record GetByIdLiveStreamQuery(long Id) : IRequest<LiveStreamGetByIdDto>;