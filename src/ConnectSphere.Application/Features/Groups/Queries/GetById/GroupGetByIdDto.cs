using ConnectSphere.Domain.Entities;
using ConnectSphere.Domain.ValueObjects;

namespace ConnectSphere.Application.Features.Groups.Queries.GetById;

public record GroupGetByIdDto
{
    public required long Id { get; set; }
    public GroupName GroupName { get; set; }
    public DateTime CreatedAt { get; set; }
    public long CreatedById { get; set; }
    
    // Mapping
    public static GroupGetByIdDto Create(Group group) // Static kullanmalıyız çünkü instance oluşturmadan çağıracağız.
    {
        return new GroupGetByIdDto
        {
            Id = group.Id,
            GroupName = group.GroupName,
            CreatedAt = group.CreatedAt,
            CreatedById = group.CreatedById
        };
    }
    public GroupGetByIdDto(long id, GroupName groupName, DateTime createdAt, long createdById)
    { 
        Id = id; 
        GroupName = groupName; 
        CreatedAt = createdAt; 
        CreatedById = createdById;
    } 
    public GroupGetByIdDto()
    {
        
    }
}

