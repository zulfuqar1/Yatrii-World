using YatriiWorld.MVC.Services;
using YatriiWorld.MVC.Services.Implementations;
using YatriiWorld.MVC.Services.Interfaces;

namespace YatriiWorld.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            
            builder.Services.AddHttpClient("YatriiApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7029/api/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
            builder.Services.AddScoped<ITourClientService, TourClientService>();
            builder.Services.AddScoped<IAccountClientService, AccountClientService>();
            builder.Services.AddScoped<ITicketClientService, TicketClientService>();
            builder.Services.AddHttpContextAccessor();

          
            builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Home/Login"; 
                });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

         
            app.MapControllerRoute(
               name: "areas",
               pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
