using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YatriiWorld.Application.Interfaces.Repositories;
using YatriiWorld.Application.Interfaces.Services;
using YatriiWorld.Domain.Entities;
using YatriiWorld.Persistance.Data;
using YatriiWorld.Persistance.Implementations.Repositories;
using YatriiWorld.Persistance.Implementations.Services;


namespace YatriiWorld.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection"))
              );
            services.AddDataProtection();



            services.AddIdentity<AppUser, IdentityRole<long>>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;

                opt.User.RequireUniqueEmail = true;

                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                opt.Lockout.MaxFailedAccessAttempts = 5;
            })
                .AddRoles<IdentityRole<long>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


            services.AddScoped<ITourRepository, TourRepository>();

            services.AddScoped<ITourImageRepository, TourImageRepository>();

            services.AddScoped<ITagRepository, TagRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IReviewRepository, ReviewRepository>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
          
            services.AddScoped<IWishlistRepository, WishlistRepository>();

          

            services.AddScoped<AppDbContextInitializer>();

            //others.....=>




            services.AddScoped<ITourService, TourService>();

            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }




        public static async Task<IApplicationBuilder> UseAppDbContextInitializer(this IApplicationBuilder app,IServiceScope scope)
        {
          
            var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();

                await initializer.InitializeBbContext();
                await initializer.InitializeRoleAsync();
                await initializer.InitializeAdmin();

                return app;
            

            
            
        }

   
    }
}