using ConnectSphere.Domain.ValueObjects;
using MediatR;

namespace ConnectSphere.Application.Features.Groups.Commands.Create;

public sealed record CreateGroupCommand(GroupName GroupName, long CreatedById, DateTime CreatedAt) : IRequest<long>;