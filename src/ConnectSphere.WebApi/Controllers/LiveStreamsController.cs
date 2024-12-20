using Microsoft.AspNetCore.Mvc;
using ConnectSphere.Application.Features.LiveStreams.Queries.GetAll;
using MediatR;

namespace ConnectSphere.WebApi.Controllers
{
    [Route("api/live-streams")]
    [ApiController]
    public class LiveStreamsController : ControllerBase
    {
        private readonly ISender _mediator;

        public LiveStreamsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LiveStreamGetAllDto>>> GetAllAsync(long createdById, string? title, string? description, DateTime? createdAt, DateTime? updatedAt, bool isActive, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllLiveStreamQuery(createdById, title, description, createdAt, updatedAt, isActive), cancellationToken));
        }
    }
}