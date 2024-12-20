using ConnectSphere.Application.Common.Attributes;
using ConnectSphere.Domain.ValueObjects;
using MediatR;

namespace ConnectSphere.Application.Features.Users.Queries.GetAll;

[CacheOptions(absoluteExpirationMinutes: 60, slidingExpirationMinutes: 15)]
public sealed record GetAllUserQuery : IRequest<List<UserGetAllDto>>
{
    [CacheKeyPart]
    public required UserName? UserName { get; set; }

    [CacheKeyPart]
    public FullName? FullName { get; set; }

    [CacheKeyPart]
    public Email? Email { get; set; }

    [CacheKeyPart]
    public PasswordHash? PasswordHash { get; set; }

    [CacheKeyPart]
    public string? ProfilePictureUrl { get; set; }

    [CacheKeyPart]
    public DateTime? CreatedAt { get; set; }

    [CacheKeyPart]
    public DateTime? LastLoginAt { get; set; }

    [CacheKeyPart]
    public string? Role { get; set; }

    [CacheKeyPart]
    public bool? IsActive { get; set; }

    public string CacheGroup => "Users";

    public GetAllUserQuery(UserName userName, FullName fullName, Email email, PasswordHash passwordHash, string profilePictureUrl, DateTime createdAt, DateTime updatedAt, string role, bool isActive)
    {
        UserName = userName;
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        ProfilePictureUrl = profilePictureUrl;
        CreatedAt = createdAt;
        LastLoginAt = updatedAt;
        Role = role;
        IsActive = isActive;
    }

}