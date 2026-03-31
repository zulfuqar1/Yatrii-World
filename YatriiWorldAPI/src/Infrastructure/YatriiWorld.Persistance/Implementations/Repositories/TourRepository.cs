using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.Interfaces.Repositories;
using YatriiWorld.Domain.Entities;
using YatriiWorld.Persistance.Data;
using YatriiWorld.Persistance.Implementations.Repositories.Generic;

namespace YatriiWorld.Persistance.Implementations.Repositories
{
    internal class TourRepository : Repository<Tour>, ITourRepository
    {
        private readonly AppDbContext _context;
        public TourRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }



        //filtering by title, description, price range
        public async Task<List<Tour>> GetFilteredToursAsync(string search, decimal? minPrice, decimal? maxPrice)
        {
            var query = _context.Tours.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(t => t.Title.Contains(search) || t.Description.Contains(search));

            if (minPrice.HasValue)
                query = query.Where(t => t.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(t => t.Price <= maxPrice.Value);

            return await query.Include(t => t.Tags).Include(t => t.Images).ToListAsync();
        }





        //pagination.....
        public async Task<List<Tour>> GetPagedToursAsync(int pageNumber, int pageSize)
        {
            return await _context.Tours
                .Skip((pageNumber - 1) * pageSize) 
                .Take(pageSize)  
                .Include(t => t.Images)
                .ToListAsync();
        }


        //top rated tours(reviews)
        public async Task<List<Tour>> GetTopRatedToursAsync(int count)
        {
            return await _context.Tours
                .AsNoTracking() 
                .Include(t => t.Images)
                .Include(t => t.Reviews)
                .ThenInclude(r => r.AppUser)
                .OrderByDescending(t => t.Reviews.Select(r => (double?)r.Rating).Average() ?? 0) 
                .Take(count)
                .ToListAsync();
        }


        public async Task<Tour> GetTourByIdWithDetailsAsync(long id)
        {
    
            return await _context.Tours
        
                .Include(t => t.Tags)
       
                .Include(t => t.Images)
       
                .Include(t => t.Category)
       
                .Include(t => t.Reviews)
                .ThenInclude(r => r.AppUser)
                .FirstOrDefaultAsync(t => t.Id == id);                                            
        }




        //category
        public async Task<int> GetTourCountByCategoryAsync(long categoryId)
        {
            return await _context.Tours.CountAsync(t => t.CategoryId == categoryId);
        }






        //standard all tours with details

        public async Task<IEnumerable<Tour>> GetToursWithDetailsAsync()
        {
            return await _context.Tours
                .Include(t => t.Tags)          
                .Include(t => t.Images)       
                .Include(t => t.Category)     
                .AsNoTracking()             
                .ToListAsync();
        }
    }
}
