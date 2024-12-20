using Microsoft.AspNetCore.Mvc;
using ConnectSphere.Application.Features.Users.Queries.GetAll;
using MediatR;
using ConnectSphere.Domain.ValueObjects;

namespace ConnectSphere.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender _mediator;

        public UsersController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserGetAllDto>>> GetAllAsync(UserName userName, FullName fullName, Email email, PasswordHash passwordHash, string profilePictureUrl, DateTime createdAt, DateTime updatedAt, string role, bool isActive, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllUserQuery(userName, fullName, email, passwordHash, profilePictureUrl, createdAt, updatedAt, role, isActive)
            {
                UserName = null
            }, cancellationToken));
        }
    }
}