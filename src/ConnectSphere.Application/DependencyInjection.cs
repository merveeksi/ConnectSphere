using System.Reflection;
using ConnectSphere.Application.Common.PiplineBehaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ConnectSphere.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // MediatR yapılandırması
            services.AddMediatR(config =>
            {
                // Mevcut assembly'deki tüm handler'ları kaydet
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                // Validasyon davranışını pipeline'a ekle
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                // Cache davranışını pipeline'a ekle
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            });

            // FluentValidation için mevcut assembly'deki tüm validator'ları kaydet
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}