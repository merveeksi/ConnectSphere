using ConnectSphere.Domain.Common.Entities;

namespace ConnectSphere.Domain.Entities;

public sealed class Like : EntityBase<long>
{
    public long PostId { get; set; }
    public Post Post { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }

    public Like()
    {
        
    }
    
    public Like(long postId, Post post, long userId, User user)
    {
        PostId = postId;
        Post = post;
        UserId = userId;
        User = user;
    }
} 