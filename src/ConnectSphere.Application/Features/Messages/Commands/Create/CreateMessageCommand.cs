using ConnectSphere.Domain.ValueObjects;
using MediatR;

namespace ConnectSphere.Application.Features.Messages.Commands.Create;

public sealed record CreateMessageCommand(long SenderId, long ReceiverId, Content Content) : IRequest<long>; 