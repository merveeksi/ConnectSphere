using ConnectSphere.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Messages.Queries.GetById;

public sealed class GetByIdMessageQueryHandler : IRequestHandler<GetByIdMessageQuery, MessageGetByIdDto>
{
    private readonly IApplicationDbContext _context;

    public GetByIdMessageQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<MessageGetByIdDto> Handle(GetByIdMessageQuery request, CancellationToken cancellationToken)
    {
        var message = await _context
            .Messages
            .AsNoTracking()
            .Select(x => new MessageGetByIdDto(x.Id, x.SenderId, x.ReceiverId, x.Content.Value, x.SentAt, x.IsRead)
            {
                Id = 0,
                SenderId = 0,
                ReceiverId = 0
            })
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        return message!;
    }
}