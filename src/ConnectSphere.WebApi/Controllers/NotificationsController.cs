using Microsoft.AspNetCore.Mvc;
using ConnectSphere.Application.Features.Notifications.Queries.GetAll;
using MediatR;

namespace ConnectSphere.WebApi.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly ISender _mediator;

        public NotificationsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<NotificationGetAllDto>>> GetAllAsync(long userId, string? title, string? content, bool isRead, DateTime? createdAt, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllNotificationQuery(userId, title, content, isRead, createdAt), cancellationToken));
        }
    }
}