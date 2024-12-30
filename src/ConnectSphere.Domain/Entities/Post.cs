using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.Enum;

namespace ConnectSphere.Domain.Entities;

public sealed class Post : EntityBase<long>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public long AuthorId { get; set; }
    public PostType Type { get; set; }
    public bool IsActive { get; set; }
    
    // Kullanıcı ilişkisi
    public long UserId { get; set; }
    public User User { get; set; }
    
    // Medya ilişkisi (fotoğraf, video vb.)
    public ICollection<Media> Medias { get; set; }
    
    // Yorum ilişkisi
    public ICollection<Comment> Comments { get; set; }
    
    // Beğeni ilişkisi
    public ICollection<Like> Likes { get; set; }
    
    // Paylaşım türüne göre ilişkiler
    public long? EventId { get; set; }
    public Event Event { get; set; }
    
    public long? GroupId { get; set; }
    public Group Group { get; set; }
    
    public long? PageId { get; set; }
    public Page Page { get; set; }
    
    public Post()
    {
        IsActive = true;
        Medias = new List<Media>();
        Comments = new List<Comment>();
        Likes = new List<Like>();
    }
    
    public Post(long id, string title, string content, PostType type, bool isActive, long userId, User user)
    {
        Id = id;
        Title = title;
        Content = content;
        Type = type;
        IsActive = isActive;
        UserId = userId;
        User = user;
        Medias = new List<Media>();
        Comments = new List<Comment>();
        Likes = new List<Like>();
    }
}