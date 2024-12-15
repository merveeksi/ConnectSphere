using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });

            // FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}