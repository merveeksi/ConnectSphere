using Microsoft.AspNetCore.Mvc;
using ConnectSphere.Application.Features.Media.Queries.GetAll;
using MediatR;

namespace ConnectSphere.WebApi.Controllers
{
    [Route("api/media")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly ISender _mediator;

        public MediaController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<MediaGetAllDto>>> GetAllAsync(long uploadedById, string? url, string? mediaType, string? fileSize, DateTime? uploadedAt, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllMediaQuery(uploadedById, url, mediaType, fileSize, uploadedAt), cancellationToken));
        }
    }
}