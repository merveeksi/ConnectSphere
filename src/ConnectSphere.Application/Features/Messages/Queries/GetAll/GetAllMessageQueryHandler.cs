using ConnectSphere.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Messages.Queries.GetAll;

public sealed class GetAllMessageQueryHandler : IRequestHandler<GetAllMessageQuery, List<MessageGetAllDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllMessageQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<MessageGetAllDto>> Handle(GetAllMessageQuery request, CancellationToken cancellationToken)
    {
        return _context
            .Messages
            .AsNoTracking()
            .Where(x => x.SenderId == request.SenderId)
            .Select(x => MessageGetAllDto.Create(x))
            .ToListAsync(cancellationToken);
    }
}