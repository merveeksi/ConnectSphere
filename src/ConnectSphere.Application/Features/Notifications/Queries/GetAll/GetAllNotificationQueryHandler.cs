using ConnectSphere.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Features.Notifications.Queries.GetAll;

public sealed class GetAllNotificationQueryHandler : IRequestHandler<GetAllNotificationQuery, List<NotificationGetAllDto>>
{
    private readonly IApplicationDbContext _context;

    public GetAllNotificationQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public Task<List<NotificationGetAllDto>> Handle(GetAllNotificationQuery request, CancellationToken cancellationToken)
    {
        return _context
            .Notifications
            .AsNoTracking()
            .Where(x => x.UserId == request.UserId)
            .Select(x => new NotificationGetAllDto(x.Id, x.UserId, x.Content, x.NotificationType, x.IsRead, x.SentAt)
            {
                Id = 0,
                UserId = 0
            })
            .ToListAsync(cancellationToken);
    }
}