using Microsoft.AspNetCore.Authentication.Cookies;
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

   
            services.AddScoped<IAccountClientService, AccountClientService>();
            services.AddScoped<ITourClientService, TourClientService>();


            services.AddHttpClient("YatriiApiClient", client =>
            {
                var baseUrl = configuration["ApiSettings:BaseUrl"];
                if (string.IsNullOrEmpty(baseUrl)) throw new Exception("API BaseUrl is missing in appsettings.json!");

                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

          
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "YatriiWorld.Auth";
                    options.LoginPath = "/Home/Login"; 
                    options.AccessDeniedPath = "/Home/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromHours(1);
                    options.SlidingExpiration = true; 
                });

            services.AddExceptionHandler<MvcExceptionHandler>();
            services.AddProblemDetails();

            return services;
        }
    }
}
