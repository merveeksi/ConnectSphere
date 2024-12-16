using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ConnectSphere.Infrastructure.Persistence.EntityFramework.Contexts;

public sealed class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        // Build configuration from appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // Create options for the ApplicationDbContext
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // Configure the DbContext to use PostgreSQL with specified connection string and naming convention
        optionsBuilder.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsHistoryTable("__ef_migrations_history"))
            .UseSnakeCaseNamingConvention();

        // Return a new instance of ApplicationDbContext with the configured options and a NullPublisher
        return new ApplicationDbContext(optionsBuilder.Options, new NullPublisher());
    }
}

// Implementation of a NullPublisher for design-time services
internal class NullPublisher : IPublisher
{
    public Task Publish(object notification, CancellationToken cancellationToken = default) => Task.CompletedTask;
    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification => Task.CompletedTask;
}
