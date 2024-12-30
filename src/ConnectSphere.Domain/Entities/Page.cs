using ConnectSphere.Domain.Common.Entities;

namespace ConnectSphere.Domain.Entities;

public sealed class Page : EntityBase<long>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
    
    // İlişkiler
    public ICollection<Post> Posts { get; set; }

    public Page()
    {
        Posts = new List<Post>();
    }
    
    public Page(long id, string title, string description, string url)
    {
        Id = id;
        Title = title;
        Description = description;
        Url = url;
    }

    public static Page Create(string title, string description, string url)
    {
        return new Page(0, title, description, url);
    }
} 