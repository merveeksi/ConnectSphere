using ConnectSphere.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.LiveStreams.Queries.GetAll;

public sealed class GetAllLiveStreamQueryHandler : IRequestHandler<GetAllLiveStreamQuery, List<LiveStreamGetAllDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllLiveStreamQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<LiveStreamGetAllDto>> Handle(GetAllLiveStreamQuery request, CancellationToken cancellationToken)
    {
        var query = _context.LiveStreams.AsQueryable();

        query = query.Where(x => x.HostId == request.HostId);

        if(!string.IsNullOrEmpty(request.Title))
            query = query.Where(x => x.Title.ToLower().Contains(request.Title.ToLower()));

        if(!string.IsNullOrEmpty(request.StreamUrl))
            query = query.Where(x => x.StreamUrl.ToLower().Contains(request.StreamUrl.ToLower()));

        if(request.StartedAt.HasValue)
            query = query.Where(x => x.StartedAt == request.StartedAt.Value);

        if(request.EndedAt.HasValue)
            query = query.Where(x => x.EndedAt == request.EndedAt.Value);

        if(request.IsActive)
            query = query.Where(x => x.IsActive == request.IsActive);

        return query
        .AsNoTracking()
        .Select(x => new LiveStreamGetAllDto(x.Id, x.HostId, x.Title, x.StreamUrl, x.StartedAt, x.EndedAt, x.IsActive)
        {
            Id = 0,
            HostId = 0
        })
        .ToListAsync(cancellationToken);
    }
}