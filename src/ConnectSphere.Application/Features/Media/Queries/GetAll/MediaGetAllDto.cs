namespace ConnectSphere.Application.Features.Media.Queries.GetAll;

public sealed record MediaGetAllDto
{
    public required long Id { get; set; }
    public required long UploadedById { get; set; }
    public required string Url { get; set; }
    public  string MediaType { get; set; }
    public string FileSize { get; set; }
    public  DateTime UploadedAt { get; set; }

    public static MediaGetAllDto Create(Domain.Entities.Media media)
    {
        return new MediaGetAllDto
        {
            Id = media.Id,
            UploadedById = media.UploadedById,
            Url = media.Url,
            MediaType = media.MediaType,
            FileSize = media.FileSize,
            UploadedAt = media.UploadedAt
        };
    }

    public MediaGetAllDto(long id, long uploadedById, string url, string mediaType, string fileSize, DateTime uploadedAt)
    {
        Id = id;
        UploadedById = uploadedById;
        Url = url;
        MediaType = mediaType;
        FileSize = fileSize;
        UploadedAt = uploadedAt;
    }

    public MediaGetAllDto() { }
}