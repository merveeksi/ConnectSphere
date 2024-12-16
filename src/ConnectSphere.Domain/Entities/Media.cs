using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.DomainEvents;
using TSID.Creator.NET;
using ConnectSphere.Domain.ValueObjects;

namespace ConnectSphere.Domain.Entities;

public sealed class Media : EntityBase<long>
{
    public long UploadedById { get; private set; } // Yükleyen kullanıcı ID
    public string Url { get; private set; } // Medya dosyasının URL'si (Azure Blob Storage URL'si)
    public string MediaType { get; private set; } // Medya türü (ör. "image", "video")
    public string FileSize { get; private set; } // Dosya boyutu
    public DateTime UploadedAt { get; private set; } // Yüklenme tarihi

    // Navigations
    public User UploadedBy { get; private set; }
    
    public static Media Create(long uploadedById, string url, string mediaType, string fileSize)
    {
        var media = new Media
        {
            Id = TsidCreator.GetTsid().ToLong(),
            UploadedById = uploadedById,
            Url = url,
            MediaType = mediaType,
            FileSize = fileSize,
            UploadedAt = DateTime.UtcNow
        };

        media.RaiseDomainEvent(new MediaUploadedDomainEvent(media));

        return media;
    }
    
    public void Update(long uploadedById, string url, string mediaType, string fileSize)
    {
        UploadedById = uploadedById;
        Url = url;
        MediaType = mediaType;
        FileSize = fileSize;
    }
    
    public void Delete()
    {
        RaiseDomainEvent(new MediaDeletedDomainEvent(this));
    }

    // Bu metot aslında var. 
    public static Media Create(long requestUploadedById, string requestUrl, string requestMediaType)
    {
        throw new NotImplementedException();
    }
}