using MediatR;

namespace ConnectSphere.Application.Features.Media.Commands.Create;

public sealed record CreateMediaCommand(long UploadedById, string Url, string MediaType, string FileSize, DateTime UploadedAt) : IRequest<long>
{
} 