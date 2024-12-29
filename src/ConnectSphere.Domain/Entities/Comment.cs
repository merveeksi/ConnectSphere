using ConnectSphere.Domain.Common.Entities;

namespace ConnectSphere.Domain.Entities;

public sealed class Comment : EntityBase<long>
{
    public Comment(long postId, Post post, long userId, User user, string content)
    {
        PostId = postId;
        Post = post;
        UserId = userId;
        User = user;
        Content = content;
    }

    public Comment()
    {
        
    }

    public long PostId { get; set; }
    public Post Post { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public string Content { get; set; }
} 