using MediatR;

namespace ConnectSphere.Application.Features.Users.Queries.GetAll;

public sealed record GetAllUserQuery(long Id) : IRequest<List<UserGetAllDto>>;