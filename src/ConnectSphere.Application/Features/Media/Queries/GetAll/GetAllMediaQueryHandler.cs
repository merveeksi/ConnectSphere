using ConnectSphere.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Media.Queries.GetAll;

public sealed class GetAllMediaQueryHandler : IRequestHandler<GetAllMediaQuery, List<MediaGetAllDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllMediaQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<MediaGetAllDto>> Handle(GetAllMediaQuery request, CancellationToken cancellationToken)
    {
        return _context
            .Media
            .AsNoTracking()
            .Where(x => x.UploadedById == request.UploadedById)
            .Select(x => MediaGetAllDto.Create(x))
            .ToListAsync(cancellationToken);
    }
}