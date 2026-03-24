using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using YatriiWorld.Application.Interfaces.Services;

namespace YatriiWorld.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
           services.AddAutoMapper(Assembly.GetExecutingAssembly());



            return services;
        }

    }
}