using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;

namespace YatriiWorld.API
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddDataProtection();
            return services;
        }
    }
}