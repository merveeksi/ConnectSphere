using ConnectSphere.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.LiveStreams.Queries.GetById;

public sealed class GetByIdLiveStreamQueryHandler : IRequestHandler<GetByIdLiveStreamQuery, LiveStreamGetByIdDto>
{
    private readonly IApplicationDbContext _context;

    public GetByIdLiveStreamQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<LiveStreamGetByIdDto> Handle(GetByIdLiveStreamQuery request, CancellationToken cancellationToken)
    {
        var liveStream = await _context
            .LiveStreams
            .AsNoTracking()
            .Select(x => new LiveStreamGetByIdDto(x.Id, x.HostId, x.Title, x.StreamUrl, x.StartedAt, x.EndedAt, x.IsActive)
            {
                Id = 0,
                HostId = 0,
            })
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        return liveStream!;
    }
}