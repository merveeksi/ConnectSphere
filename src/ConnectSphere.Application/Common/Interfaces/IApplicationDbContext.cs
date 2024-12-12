using ConnectSphere.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Group> Groups { get; set; }
    DbSet<LiveStream> LiveStreams { get; set; }
    DbSet<Media> Media { get; set; }
    DbSet<Message> Messages { get; set; }
    DbSet<Notification> Notifications { get; set; }
    DbSet<User> Users { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    int SaveChanges();
}