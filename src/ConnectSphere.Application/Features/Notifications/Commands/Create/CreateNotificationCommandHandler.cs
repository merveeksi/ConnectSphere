using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Entities;
using MediatR;

namespace ConnectSphere.Application.Features.Notifications.Commands.Create;

public sealed class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, long>
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheInvalidator _cacheInvalidator;

    public CreateNotificationCommandHandler(IApplicationDbContext context, ICacheInvalidator cacheInvalidator)
    {
        _context = context;
        _cacheInvalidator = cacheInvalidator;
    }

    public async Task<long> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var notification = Notification.Create(request.UserId, request.Content, request.NotificationType);
        _context.Notifications.Add(notification);
        await _context.SaveChangesAsync(cancellationToken);
        await _cacheInvalidator.InvalidateGroupAsync("Notifications", cancellationToken);
        return notification.Id;
    }
} 