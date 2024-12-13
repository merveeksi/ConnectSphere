using ConnectSphere.Domain.ValueObjects;
using MediatR;

namespace ConnectSphere.Application.Features.Groups.Queries.GetAll;

public sealed record GetAllGroupQuery : IRequest<List<GroupGetAllDto>>
{
    public long CreatedById { get; set; }
    public GroupName? GroupName { get; set; }
    public DateTime? CreatedAt { get; set; }
}