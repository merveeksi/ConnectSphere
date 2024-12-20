using ConnectSphere.Application.Common.Attributes;
using MediatR;

namespace ConnectSphere.Application.Features.Media.Queries.GetAll;

[CacheOptions(absoluteExpirationMinutes: 60, slidingExpirationMinutes: 15)]
public sealed record GetAllMediaQuery : IRequest<List<MediaGetAllDto>>
{
    [CacheKeyPart]
    public long UploadedById { get; set; }

    [CacheKeyPart]
    public string? Url { get; set; }

    [CacheKeyPart]
    public string? MediaType { get; set; }

    [CacheKeyPart]
    public string? FileSize { get; set; }

    [CacheKeyPart]
    public DateTime? UploadedAt { get; set; }

    public string CacheGroup => "Media";
    public GetAllMediaQuery(long uploadedById, string? url, string? mediaType, string? fileSize, DateTime? uploadedAt)
    {
        UploadedById = uploadedById;
        Url = url;
        MediaType = mediaType;
        FileSize = fileSize;
        UploadedAt = uploadedAt;
    }
}