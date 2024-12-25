using ConnectSphere.Application.Common.Interfaces;
using ConnectSphere.Domain.Settings;
using ConnectSphere.Infrastructure.Persistence.EntityFramework.Contexts;
using ConnectSphere.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resend;
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

        // JWT Authentication
        ConfigureJwtSettings(services, configuration);
        
        services.AddScoped<IJwtService, JwtManager>();
        services.AddScoped<IIdentityService, IdentityManager>();
        services.AddScoped<IEmailService, ResendEmailManager>();
        services.AddIdentity<AppUser, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
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
            
       // Resend
       services.AddOptions();
       services.AddHttpClient<ResendClient>();
       services.Configure<ResendClientOptions>(o => o.ApiToken = configuration.GetSection("ResendApiKey").Value!);
       services.AddTransient<IResend, ResendClient>();

        return services;
    }

    private static void ConfigureJwtSettings(IServiceCollection services, IConfiguration configuration)
    {
        // Retrieve JWT settings section from configuration
        var jwtSettingsSection = configuration.GetSection("JwtSettings");
        // If JWT settings exist in configuration, use those settings
        if (jwtSettingsSection.Exists())
        {
            services.Configure<JwtSettings>(jwtSettingsSection);
        }
        // Otherwise, configure JWT settings with default values
        else
        {
            services.Configure<JwtSettings>(options =>
            {
                options.SecretKey = "default-secret-key-for-development-only";
                options.AccessTokenExpiration = TimeSpan.FromMinutes(30);
                options.RefreshTokenExpiration = TimeSpan.FromDays(7);
                options.Issuer = "ConnectSphere";
                options.Audience = "ConnectSphere";
            });
        }
    }
}