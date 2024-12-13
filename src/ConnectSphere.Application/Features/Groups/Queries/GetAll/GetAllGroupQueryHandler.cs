using ConnectSphere.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Groups.Queries.GetAll;

public sealed class GetAllGroupQueryHandler : IRequestHandler<GetAllGroupQuery, List<GroupGetAllDto>>
{
    private readonly IApplicationDbContext _context;
    
    public GetAllGroupQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<GroupGetAllDto>> Handle(GetAllGroupQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Groups.AsQueryable();
        
        query = query.Where(x => x.CreatedById == request.CreatedById);
        
        if (!string.IsNullOrEmpty(request.GroupName))
            query = query.Where(x => x.GroupName.Value.ToLower().Contains(request.GroupName.Value.ToLower()));
        
        if (request.CreatedAt.HasValue)
            query = query.Where(x => x.CreatedAt.Date == request.CreatedAt.Value.Date);

        return query
            .AsNoTracking()
            .Select(x => new GroupGetAllDto(x.Id, x.GroupName, x.CreatedAt, x.CreatedById)
            {
                Id = 0
            })
            .ToListAsync(cancellationToken);
    }
}
