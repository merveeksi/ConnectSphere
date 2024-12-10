using MediatR;

namespace ConnectSphere.Application.Features.LiveStreams.Commands.Create;

public sealed record CreateLiveStreamCommand(long HostId, string Title, string StreamUrl) : IRequest<long>;