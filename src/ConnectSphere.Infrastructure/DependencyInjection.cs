using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Infrastructure.Persistence.EntityFramework.Contexts;
using ConnectSphere.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace ConnectSphere.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.
            AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsHistoryTable("__ef_migrations_history", "connect_sphere"))
                .UseSnakeCaseNamingConvention());

        // Add scoped services
        services.
            AddScoped<IApplicationDbContext, ApplicationDbContext>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
            options.InstanceName = "ConnectSphere_";
        });
        
        services.AddSingleton<ICacheKeyFactory, CacheKeyFactory>();

        services.AddScoped<ICacheInvalidator, CacheInvalidator>();

       // services.AddScoped<IObjectStorage, S3ObjectStorageManager>();

        // Register Redis connection for advanced operations
        services.AddSingleton<IConnectionMultiplexer>(sp =>
            ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")));

       // services.Configure<S3Settings>(bind => configuration.GetSection("S3Settings").Bind(bind));
            
        return services;
    }
}