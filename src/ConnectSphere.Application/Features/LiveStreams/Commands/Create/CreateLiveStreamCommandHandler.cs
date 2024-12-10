using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Entities;
using MediatR;

namespace ConnectSphere.Application.Features.LiveStreams.Commands.Create;

public sealed class CreateLiveStreamCommandHandler : IRequestHandler<CreateLiveStreamCommand, long>
{
    private readonly IApplicationDbContext _context;

    public CreateLiveStreamCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(CreateLiveStreamCommand request, CancellationToken cancellationToken)
    {
        var liveStream = LiveStream.Create(request.HostId, request.Title, request.StreamUrl);
        _context.LiveStreams.Add(liveStream);
        await _context.SaveChangesAsync(cancellationToken);
        return liveStream.Id;
    }
} 