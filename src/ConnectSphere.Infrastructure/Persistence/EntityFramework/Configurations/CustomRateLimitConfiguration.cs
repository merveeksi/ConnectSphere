using AspNetCoreRateLimit;
using ConnectSphere.Application.Common.Interfaces;

namespace ConnectSphere.Infrastructure.Persistence.EntityFramework.Configurations;

public sealed class CustomRateLimitConfiguration: ICustomRateLimitConfiguration
{
    public void ConfigureRateLimits(IpRateLimitOptions options)
    {
        // Özel rate limit kuralları
        options.GeneralRules.Add(new RateLimitRule
        {
            Endpoint = "*/api/high-priority/*",
            Period = "1m",
            Limit = 100
        });
    }
}