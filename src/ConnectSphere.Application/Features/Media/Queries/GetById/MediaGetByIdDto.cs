namespace ConnectSphere.Application.Features.Media.Queries.GetById;

public sealed record MediaGetByIdDto
{
    public required long Id { get; set; }
    public required long UploadedById { get; set; }
    public required string Url { get; set; }
    public  string MediaType { get; set; }
    public  DateTime UploadedAt { get; set; }

    
    public static MediaGetByIdDto Create(Domain.Entities.Media media)
    {
        return new MediaGetByIdDto
        {
            Id = media.Id,
            UploadedById = media.UploadedById,
            Url = media.Url,
            MediaType = media.MediaType,
            UploadedAt = media.UploadedAt
        };
    }

    public MediaGetByIdDto(long id, long uploadedById, string url, string mediaType, DateTime uploadedAt)
    {
        Id = id;
        UploadedById = uploadedById;
        Url = url;
        MediaType = mediaType;
        UploadedAt = uploadedAt;
    }
    public MediaGetByIdDto()
    {
    }
}