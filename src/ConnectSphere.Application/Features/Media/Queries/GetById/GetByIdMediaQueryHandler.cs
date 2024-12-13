using ConnectSphere.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Media.Queries.GetById;

public sealed class GetByIdMediaQueryHandler : IRequestHandler<GetByIdMediaQuery, MediaGetByIdDto>
{
    private readonly IApplicationDbContext _context;

    public GetByIdMediaQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<MediaGetByIdDto> Handle(GetByIdMediaQuery request, CancellationToken cancellationToken)
    {
        var media = await _context
            .Media
            .AsNoTracking()
            .Select(x => new MediaGetByIdDto(x.Id, x.UploadedById, x.Url, x.MediaType, x.FileSize, x.UploadedAt)
            {
                Id = 0,
                UploadedById = 0,
                Url = "",
            })
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        return media!;
    }
}