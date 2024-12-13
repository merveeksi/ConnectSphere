using MediatR;

namespace ConnectSphere.Application.Features.Media.Queries.GetAll;

public sealed record GetAllMediaQuery : IRequest<List<MediaGetAllDto>>
{
    public long UploadedById { get; set; }
    public string? Url { get; set; }
    public string? MediaType { get; set; }
    public string? FileSize { get; set; }
    public DateTime? UploadedAt { get; set; }
}