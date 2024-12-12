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
        return _context
            .LiveStreams
            .AsNoTracking()
            .Where(x => x.HostId == request.HostId)
            .Select(x => LiveStreamGetAllDto.Create(x))
            .ToListAsync(cancellationToken);
    }
}