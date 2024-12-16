using System.Reflection;
using ConnectSphere.Application.Common.PiplineBehaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ConnectSphere.Application
{
    /// <summary>
    /// Uygulama katmanının bağımlılıklarını yapılandıran statik sınıf
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Uygulama katmanı için gerekli servisleri DI container'a kaydeder
        /// </summary>
        /// <param name="services">Servis koleksiyonu</param>
        /// <returns>Yapılandırılmış servis koleksiyonu</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // MediatR yapılandırması
            services.AddMediatR(config =>
            {
                // Mevcut assembly'deki tüm handler'ları kaydet
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                // Validasyon davranışını pipeline'a ekle
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            // FluentValidation için mevcut assembly'deki tüm validator'ları kaydet
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}