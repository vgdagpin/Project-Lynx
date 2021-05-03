using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using TasqR;

namespace Lynx.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, bool includeValidators = false)
        {
            services.AddTasqR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
