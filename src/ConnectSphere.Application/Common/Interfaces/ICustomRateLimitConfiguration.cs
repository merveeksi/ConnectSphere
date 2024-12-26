using AspNetCoreRateLimit;

namespace ConnectSphere.Application.Common.Interfaces;

public interface ICustomRateLimitConfiguration
{
    void ConfigureRateLimits(IpRateLimitOptions options);
}