using ConnectSphere.Application.Features.Groups.Queries.GetById;
using ConnectSphere.Domain.Entities;
using ConnectSphere.Domain.ValueObjects;

namespace ConnectSphere.Application.Features.Groups.Queries.GetAll;

public sealed record GroupGetAllDto
{
    public required long Id { get; set; }
    public GroupName GroupName { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedById { get; set; }


    public static GroupGetAllDto Create(Group group)
    {
        return new GroupGetAllDto
        {
            Id = group.Id,
            GroupName = group.GroupName,
            CreatedAt = group.CreatedAt,
            CreatedById = group.CreatedById
        };
    }

    public GroupGetAllDto(long id, GroupName groupName, DateTime createdAt, long createdById)
    { 
        Id = id; 
        GroupName = groupName; 
        CreatedAt = createdAt; 
        CreatedById = createdById;
    } 
    public GroupGetAllDto()
    {
        
    }
}