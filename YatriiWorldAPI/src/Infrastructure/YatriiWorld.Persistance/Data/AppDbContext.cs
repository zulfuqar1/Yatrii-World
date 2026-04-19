using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Domain.Entities;
using YatriiWorld.Persistance.Data.Common;


namespace YatriiWorld.Persistance.Data
{
     public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<long>, long>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
      

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.ApplyAllQueriesFilters();
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<Category> Categories { get; set; }

        public DbSet<Tour> Tours { get; set; }

      
        public DbSet<Tag> Tags { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<TourImage> TourImages { get; set; }

        public DbSet<ContactMessage> ContactMessages { get; set; }

        public DbSet<Wishlist> Wishlists { get; set; }

        public DbSet<TicketTraveler> TicketTravelers { get; set; }

     
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        public DbSet<ProductTag> ProductTags { get; set; }
    }
}