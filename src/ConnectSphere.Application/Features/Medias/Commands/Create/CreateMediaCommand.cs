using MediatR;

namespace ConnectSphere.Application.Features.Medias.Commands.Create;

public sealed record CreateMediaCommand(long UploadedById, string Url, string MediaType) : IRequest<long>; 