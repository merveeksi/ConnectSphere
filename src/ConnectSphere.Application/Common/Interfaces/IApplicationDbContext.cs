using ConnectSphere.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<ChatRoom> ChatRooms { get; set; }
    public DbSet<Comment> Comments {get; set;}
    public DbSet<Event> Events { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<LiveStream> LiveStreams { get; set; }
    public DbSet<Media> Media { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<RoomCategory> RoomCategories { get; set; }
    public DbSet<RoomParticipant> RoomParticipants { get; set; }
    public DbSet<RoomTag> RoomTags { get; set; }
    public DbSet<Story> Stories { get; set; }
    public DbSet<User> Users { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
}