using ConnectSphere.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Groups.Queries.GetById;

public sealed class GetByIdGroupQueryHandler : IRequestHandler<GetByIdGroupQuery, GroupGetByIdDto>
{
    private readonly IApplicationDbContext _context;

    public GetByIdGroupQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<GroupGetByIdDto> Handle(GetByIdGroupQuery request, CancellationToken cancellationToken)
    {
        var group = await _context
            .Groups
            .AsNoTracking() // Performans kazancı sağlar.
            .Select(x => new GroupGetByIdDto(x.Id, x.GroupName, x.CreatedAt, x.CreatedById)
            {
                Id = 0
            })
            .FirstOrDefaultAsync(x=>x.Id == request.Id, cancellationToken);
        
        return group!;
    }
}