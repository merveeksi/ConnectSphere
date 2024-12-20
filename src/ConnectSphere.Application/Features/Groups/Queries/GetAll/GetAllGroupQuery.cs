using ConnectSphere.Application.Common.Attributes;
using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.ValueObjects;
using MediatR;

namespace ConnectSphere.Application.Features.Groups.Queries.GetAll;

[CacheOptions(absoluteExpirationMinutes: 60, slidingExpirationMinutes: 15)]
public sealed record GetAllGroupQuery : IRequest<List<GroupGetAllDto>> , ICacheable
{
    [CacheKeyPart]
    public long CreatedById { get; set; }
    [CacheKeyPart]
    public GroupName? GroupName { get; set; }
    [CacheKeyPart]
    public DateTime? CreatedAt { get; set; }
    public string CacheGroup => "Groups";
    public GetAllGroupQuery(long createdById, GroupName? groupName, DateTime? createdAt)
    {
        CreatedById = createdById;
        GroupName = groupName;
        CreatedAt = createdAt;
    }
}