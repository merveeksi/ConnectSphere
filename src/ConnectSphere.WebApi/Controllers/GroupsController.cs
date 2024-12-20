using ConnectSphere.Application.Features.Groups.Queries.GetAll;
using ConnectSphere.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConnectSphere.WebApi.Controllers;

[Route("api/groups")]
[ApiController]
public class GroupsController : Controller
{
    private readonly ISender _mediator;
    
    public GroupsController(ISender mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<GroupGetAllDto>>> GetAllAsync(long createdById, GroupName? groupName, DateTime createdAt, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetAllGroupQuery(createdById, groupName, createdAt), cancellationToken));
    }
}