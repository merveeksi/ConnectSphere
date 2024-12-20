using Microsoft.AspNetCore.Mvc;
using ConnectSphere.Application.Features.Messages.Queries.GetAll;
using MediatR;

namespace ConnectSphere.WebApi.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ISender _mediator;

        public MessagesController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<MessageGetAllDto>>> GetAllAsync(long senderId, long? receiverId, string? content, DateTime? sentAt, bool isRead, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllMessageQuery(senderId, receiverId, content, sentAt, isRead), cancellationToken));
        }
    }
}