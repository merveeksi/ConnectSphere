using MediatR;

namespace ConnectSphere.Application.Features.Messages.Queries.GetById;

public record GetByIdMessageQuery(long Id) : IRequest<MessageGetByIdDto>;