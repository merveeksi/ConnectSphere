using MediatR;

namespace ConnectSphere.Application.Features.Users.Queries.GetById;

public record GetByIdUserQuery(long Id) : IRequest<UserGetByIdDto>;