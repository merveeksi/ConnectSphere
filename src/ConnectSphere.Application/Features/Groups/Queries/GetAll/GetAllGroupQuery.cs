using MediatR;

namespace ConnectSphere.Application.Features.Groups.Queries.GetAll;

public sealed record GetAllGroupQuery(long CreatedById) : IRequest<List<GroupGetAllDto>>;