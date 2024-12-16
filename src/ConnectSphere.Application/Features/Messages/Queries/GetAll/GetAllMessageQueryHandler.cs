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
        var query = _context.Messages.AsQueryable();

        query = query.Where(x => x.SenderId == request.SenderId);

        if(request.ReceiverId.HasValue)
            query = query.Where(x => x.ReceiverId == request.ReceiverId.Value);

        if(!string.IsNullOrEmpty(request.Content))
            query = query.Where(x => x.Content.Value.ToLower().Contains(request.Content.ToLower()));

        if(request.SentAt.HasValue)
            query = query.Where(x => x.SentAt == request.SentAt.Value);

        if(request.IsRead)
            query = query.Where(x => x.IsRead == request.IsRead);

        return query
        .AsNoTracking()
        .Select(x => new MessageGetAllDto(x.Id, x.SenderId, x.ReceiverId, x.Content, x.SentAt, x.IsRead)
        {
            Id = 0,
            SenderId = 0,
            ReceiverId = 0
        })
        .ToListAsync(cancellationToken);
    }
}
