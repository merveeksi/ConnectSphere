using MediatR;

namespace ConnectSphere.Application.Features.Groups.Queries.GetById;

public record GetByIdGroupQuery(long Id) : IRequest<GroupGetByIdDto>;