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
        var query = _context.Media.AsQueryable();

        query = query.Where(x => x.UploadedById == request.UploadedById);

        if(!string.IsNullOrEmpty(request.Url))
            query = query.Where(x => x.Url.ToLower().Contains(request.Url.ToLower()));  

        if(!string.IsNullOrEmpty(request.MediaType))
            query = query.Where(x => x.MediaType.ToLower().Contains(request.MediaType.ToLower()));
        
        if(!string.IsNullOrEmpty(request.FileSize))
            query = query.Where(x => x.FileSize.ToLower().Contains(request.FileSize.ToLower()));

        if(request.UploadedAt.HasValue)
            query = query.Where(x => x.UploadedAt == request.UploadedAt.Value);

        return query.AsNoTracking().Select(x => MediaGetAllDto.Create(x)).ToListAsync(cancellationToken);
    }
}