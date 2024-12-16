namespace ConnectSphere.Application.Features.Media.Queries.GetAll;

public sealed record MediaGetAllDto
{
    public required long Id { get; set; }
    public required long UploadedById { get; set; }
    public required string Url { get; set; }
    public  string MediaType { get; set; }
    public string FileSize { get; set; }
    public  DateTime UploadedAt { get; set; }

    public static MediaGetAllDto Create(long id, long uploadedById, string url, string mediaType, string fileSize, DateTime uploadedAt)
    {
        return new MediaGetAllDto
        {
            Id = id,
            UploadedById = uploadedById,
            Url = url,
            MediaType = mediaType,
            FileSize = fileSize,
            UploadedAt = uploadedAt
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