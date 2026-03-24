using System.Net.Http.Headers;
using YatriiWorld.MVC.Middlewares;
using YatriiWorld.MVC.Services.Interfaces;

namespace YatriiWorld.MVC
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddMvcServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddHttpClient("Api", client =>
            {
                client.BaseAddress = new Uri(configuration["ApiSettings:BaseUrl"]!);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            });



            services.AddExceptionHandler<MvcExceptionHandler>();
            services.AddProblemDetails();

            return services;
        }
    }
}
