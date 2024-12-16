using ConnectSphere.Domain.Entities;
using ConnectSphere.Domain.ValueObjects;

namespace ConnectSphere.Application.Features.Users.Queries.GetAll;

public sealed record UserGetAllDto
{
    public required long Id { get; set; }
    public required UserName UserName { get; set; }
    public  FullName FullName { get; set; }
    public required Email Email { get; set; }
    public  string ProfilePictureUrl { get; set; }
    public required PasswordHash PasswordHash { get; set; }
    public  string Role { get; set; }
    public  DateTime CreatedAt { get; set; }
    public  bool IsActive { get; set; }
    public  DateTime? LastLoginAt { get; set; }

    public static UserGetAllDto Create(long id, UserName username, FullName fullName, Email email, string profilePictureUrl, PasswordHash passwordHash, string role, DateTime createdAt, bool isActive, DateTime? lastLoginAt)
    {
        return new UserGetAllDto
        {
            Id = id,
            UserName = username,
            FullName = fullName,
            Email = email,
            ProfilePictureUrl = profilePictureUrl,
            PasswordHash = passwordHash,
            Role = role,
            CreatedAt = createdAt,
            IsActive = isActive,
            LastLoginAt = lastLoginAt
        };
    }

    public UserGetAllDto(long id, UserName username, FullName fullName, Email email, string profilePictureUrl, PasswordHash passwordHash, string role, DateTime createdAt, bool isActive, DateTime? lastLoginAt)
    {
        Id = id;
        UserName = username;
        FullName = fullName;
        Email = email;
        ProfilePictureUrl = profilePictureUrl;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = createdAt;
        IsActive = isActive;
        LastLoginAt = lastLoginAt;
    }

    public UserGetAllDto() { }
}