using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Infrastructure.Persistence.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConnectSphere.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsHistoryTable("__ef_migrations_history", "connect_sphere"))
                .UseSnakeCaseNamingConvention());

        // Add scoped services
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}