using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.DomainEvents;
using TSID.Creator.NET;
using ConnectSphere.Domain.ValueObjects;

namespace ConnectSphere.Domain.Entities;

public sealed class Media : EntityBase<long>
{
    public long UploadedById { get; set; } // Yükleyen kullanıcı ID
    public string Url { get; set; } // Medya dosyasının URL'si (Azure Blob Storage URL'si)
    public string MediaType { get; set; } // Medya türü (ör. "image", "video")
    public DateTime UploadedAt { get; set; } // Yüklenme tarihi

    // Navigations
    public User UploadedBy { get; set; }

    private const long MaxFileSize = 5 * 1024 * 1024; // 5 MB
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".mp4", ".avi" }; // İzin verilen uzantılar

    public static Media Create(long uploadedById, string url, string mediaType, long fileSize)
    {
        ValidateMedia(fileSize, mediaType);
        
        var media = new Media
        {
            Id = TsidCreator.GetTsid().ToLong(),
            UploadedById = uploadedById,
            Url = url,
            MediaType = mediaType,
            UploadedAt = DateTime.UtcNow
        };

        media.RaiseDomainEvent(new MediaUploadedDomainEvent(media));

        return media;
    }
    public static Media Create(long requestUploadedById, string requestUrl, string requestMediaType)
    {
        throw new NotImplementedException();
    }

    private static void ValidateMedia(long fileSize, string mediaType) // Medya dosyasının geçerli olup olmadığını kontrol eder
    {
        if (fileSize > MaxFileSize)
            throw new ArgumentException($"File size cannot exceed {MaxFileSize / (1024 * 1024)} MB.", nameof(fileSize));

        if (!IsValidMediaType(mediaType))
            throw new ArgumentException("Invalid media type.", nameof(mediaType));
    }

    private static bool IsValidMediaType(string mediaType) // Medya türünün geçerli olup olmadığını kontrol eder
    {
        return AllowedExtensions.Any(ext => mediaType.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
    }
    
    public void Update(long uploadedById, string url, string mediaType)
    {
        UploadedById = uploadedById;
        Url = url;
        MediaType = mediaType;
    }
    
    public void Delete()
    {
        RaiseDomainEvent(new MediaDeletedDomainEvent(this));
    }
    
}