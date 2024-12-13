using ConnectSphere.Domain.ValueObjects;
using MediatR;

namespace ConnectSphere.Application.Features.Users.Queries.GetAll;

public sealed record GetAllUserQuery : IRequest<List<UserGetAllDto>>
{
    public required UserName UserName { get; set; }
    public FullName? FullName { get; set; }
    public Email? Email { get; set; }
    public PasswordHash? PasswordHash { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public string? Role { get; set; }
    public bool? IsActive { get; set; }
}