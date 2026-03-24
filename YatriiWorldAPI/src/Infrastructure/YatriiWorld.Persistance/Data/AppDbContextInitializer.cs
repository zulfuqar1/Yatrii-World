using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Domain.Entities;
using YatriiWorld.Domain.Entities.Enums;

namespace YatriiWorld.Persistance.Data
{
    internal class AppDbContextInitializer
    {
        private readonly RoleManager<IdentityRole<long>> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public AppDbContextInitializer(
            RoleManager<IdentityRole<long>> roleManager,
            UserManager<AppUser> userManager,
            IConfiguration configuration,
            AppDbContext context
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
        }

        public async Task InitializeBbContext()
        {
            if(!await _context.Database.EnsureCreatedAsync())
            {
                await _context.Database.MigrateAsync();
            }
        }


        public async Task InitializeRoleAsync()
        {
            foreach (UserRole role in Enum.GetValues(typeof(UserRole)))
            {

                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {

                    await _roleManager.CreateAsync(new IdentityRole<long> { Name = role.ToString() });

                }

            }
        }

        public async Task InitializeAdmin()
        {


            bool result = await _userManager.Users.AnyAsync(u=>u.UserName ==_configuration["AdminSettings:UserName"] || u.Email == _configuration["AdminSettings:Email"]);



            if (!result)
            {
                AppUser user = new AppUser
                {
                    UserName = _configuration["AdminSettings:UserName"],
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = _configuration["AdminSettings:Email"],
                    EmailConfirmed = true

                };
                 
                await _userManager.CreateAsync(user, _configuration["AdminSettings:Password"]);

                await _userManager.AddToRoleAsync(user, UserRole.Admin.ToString());

            }


        }

      
    }
}
