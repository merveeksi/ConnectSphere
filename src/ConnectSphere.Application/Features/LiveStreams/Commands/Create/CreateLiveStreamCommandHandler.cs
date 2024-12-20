using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Entities;
using MediatR;

namespace ConnectSphere.Application.Features.LiveStreams.Commands.Create;

public sealed class CreateLiveStreamCommandHandler : IRequestHandler<CreateLiveStreamCommand, long>
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheInvalidator _cacheInvalidator;

    public CreateLiveStreamCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
    {
        _context = context;
        _cacheInvalidator = cacheInvalidator;
    }

    public async Task<long> Handle(CreateLiveStreamCommand request, CancellationToken cancellationToken)
    {
        var liveStream = LiveStream.Create(request.HostId, request.Title, request.StreamUrl);

        _context.LiveStreams.Add(liveStream);

        await _context.SaveChangesAsync(cancellationToken);

        await _cacheInvalidator.InvalidateGroupAsync("LiveStreams", cancellationToken);
        
        return liveStream.Id;
    }
} 