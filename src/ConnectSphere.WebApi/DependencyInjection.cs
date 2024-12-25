using ConnectSphere.Application.Common.Interfaces;

namespace ConnectSphere.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
    {

        services.AddSingleton<IEnvironmentService, EnvironmentManager>(sp =>
            new EnvironmentManager(environment.WebRootPath));
        

        return services;
    }
}