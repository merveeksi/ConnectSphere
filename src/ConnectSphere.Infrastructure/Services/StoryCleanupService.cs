using ConnectSphere.Infrastructure.Persistence.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ConnectSphere.Infrastructure.Services
{
    public sealed class StoryCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        
        public StoryCleanupService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    
                    var expiredStories = await dbContext.Stories
                        .Where(s => s.ExpirationTime <= DateTime.UtcNow)
                        .ToListAsync(stoppingToken);

                    if (expiredStories.Any())
                    {
                        dbContext.Stories.RemoveRange(expiredStories);
                        await dbContext.SaveChangesAsync(stoppingToken);
                    }
                }

                // Her 5 dakikada bir kontrol et
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}