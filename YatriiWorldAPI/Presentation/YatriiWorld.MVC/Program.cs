using YatriiWorld.MVC.Services.Implementations;
using YatriiWorld.MVC.Services.Interfaces;

namespace YatriiWorld.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Standart MVC Servisleri
            builder.Services.AddControllersWithViews();

            
            builder.Services.AddHttpClient("YatriiApiClient", client =>
            {
                // Buradaki portu (7029) kendi API projenin portuyla de?i?tir!
                client.BaseAddress = new Uri("https://localhost:7029/api/");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthorization();

            // Areas deste?i (Admin paneli vb. için önemli)
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
