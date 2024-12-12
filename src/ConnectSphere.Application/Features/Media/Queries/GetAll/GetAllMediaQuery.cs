using MediatR;

namespace ConnectSphere.Application.Features.Media.Queries.GetAll;

public sealed record GetAllMediaQuery(long UploadedById) : IRequest<List<MediaGetAllDto>>;