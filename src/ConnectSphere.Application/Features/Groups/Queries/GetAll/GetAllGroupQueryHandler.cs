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
        return _context
            .Groups
            .AsNoTracking()
            .Where(x => x.CreatedById == request.CreatedById)
            .Select(x => GroupGetAllDto.Create(x))
            .ToListAsync(cancellationToken);
    }
}