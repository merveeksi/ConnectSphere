using MediatR;

namespace ConnectSphere.Application.Features.Messages.Queries.GetAll;

public sealed record GetAllMessageQuery(long SenderId) : IRequest<List<MessageGetAllDto>>;