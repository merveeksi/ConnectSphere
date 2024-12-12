using ConnectSphere.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Notifications.Queries.GetById;

public sealed class GetByIdNotificationQueryHandler : IRequestHandler<GetByIdNotificationQuery, NotificationGetByIdDto>
{
    private readonly IApplicationDbContext _context;

    public GetByIdNotificationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<NotificationGetByIdDto> Handle(GetByIdNotificationQuery request, CancellationToken cancellationToken)
    {
        var notification = await _context
            .Notifications
            .AsNoTracking()
            .Select(x => new NotificationGetByIdDto(x.Id, x.UserId, x.Content.Value, x.NotificationType, x.IsRead, x.SentAt)
            {
                Id = 0,
                UserId = 0
            })
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        
        return notification!;
    }
}