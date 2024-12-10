using ConnectSphere.Domain.ValueObjects;
using MediatR;

namespace ConnectSphere.Application.Features.Users.Commands.Create;

public sealed record CreateUserCommand(UserName Username, FullName FullName, Email Email, PasswordHash PasswordHash, string profilePictureUrl, string role)
    : IRequest<long>; 