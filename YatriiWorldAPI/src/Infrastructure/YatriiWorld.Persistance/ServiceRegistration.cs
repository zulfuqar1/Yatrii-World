using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Persistance.Data;

namespace YatriiWorld.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<AppDbContext>
                (opt=>opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));
        }


    }
}
