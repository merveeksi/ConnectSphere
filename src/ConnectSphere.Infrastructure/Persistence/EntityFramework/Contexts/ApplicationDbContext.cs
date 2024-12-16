using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Common.Entities;
using ConnectSphere.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConnectSphere.Infrastructure.Persistence.EntityFramework.Contexts;
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IPublisher _publisher;
         public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IPublisher publisher) : base(options)
        {
            _publisher = publisher;
        }
        public DbSet<Group> Groups { get; set; }
        public DbSet<LiveStream> LiveStreams { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<User> Users { get; set; }

        // DbContext'in yapılandırılması
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        // Değişiklikleri kaydederken domain eventleri dağıtır
         public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        await DispatchDomainEventsAsync(cancellationToken);

        return result;
    }


    // Domain eventleri dağıtır
    private async Task DispatchDomainEventsAsync(CancellationToken cancellationToken)
    {
        var domainEvents = ChangeTracker
        .Entries<EntityBase<long>>()
        .Select(e => e.Entity)
        .Where(e => e.GetDomainEvents().Any())
        .ToArray();

        foreach (var entity in domainEvents)
        {
            var events = entity.GetDomainEvents();

            foreach (var domainEvent in events)
                await _publisher.Publish(domainEvent, cancellationToken);

            entity.ClearDomainEvents();
        }
    }
}