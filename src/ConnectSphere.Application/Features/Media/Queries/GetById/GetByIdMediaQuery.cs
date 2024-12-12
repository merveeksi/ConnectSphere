using MediatR;

namespace ConnectSphere.Application.Features.Media.Queries.GetById;

public record GetByIdMediaQuery(long Id) : IRequest<MediaGetByIdDto>;