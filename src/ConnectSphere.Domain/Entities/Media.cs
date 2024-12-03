using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.DomainEvents;
using TSID.Creator.NET;

namespace ConnectSphere.Domain.Entities;

public sealed class Media : EntityBase<long>
{
    public long UploadedById { get; set; } // Yükleyen kullanıcı ID
    public string Url { get; set; } // Medya dosyasının URL'si (Azure Blob Storage URL'si)
    public string MediaType { get; set; } // Medya türü (ör. "image", "video")
    public DateTime UploadedAt { get; set; } // Yüklenme tarihi

    // Navigations
    public User UploadedBy { get; set; }
}

public static Media Create(long uploadedById, string url, string mediaType)
{
    var media = new Media
    {
        Id = TsidCreator.GetTsid().ToLong(),
        UploadedById = uploadedById,
        Url = url,
        MediaType = mediaType,
        UploadedAt = DateTime.UtcNow
    };

    media.RaiseDomainEvent(new MediaUploadedDomainEvent(media.Id, media.Url));
    return media;
}