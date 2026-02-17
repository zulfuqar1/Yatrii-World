using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Persistance.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Category> Categories { get; set; }

        public DbSet<Tour> Tours { get; set; }

        public DbSet<Destination> Destinations { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<TourImage> TourImages { get; set; }

        public DbSet<ContactMessage> ContactMessages { get; set; }



    }
}