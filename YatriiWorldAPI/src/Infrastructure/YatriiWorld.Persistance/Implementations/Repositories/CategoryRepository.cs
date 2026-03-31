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
    internal class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetCategoriesWithActiveToursAsync()
        {
            return await _context.Categories
                .Where(c => c.Tours.Any(t => t.IsDeleted))
                .Include(c => c.Tours.Where(t => t.IsDeleted)) 
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<Category>> GetCategoriesWithToursAsync()
        {
            return await _context.Categories
                .Include(c => c.Tours)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<IEnumerable<Category>> GetCategoryWithDetailsAsync()
        {
            return await _context.Categories
                .Include(c => c.Tours)
                    .ThenInclude(t => t.Images) 
                .Include(c => c.Tours)
                    .ThenInclude(t => t.Tags) 
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Category> GetCategoryWithToursByIdAsync(long id)
        {
            return await _context.Categories
                .Include(c => c.Tours)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<int> GetTotalTourCountAsync(long categoryId)
        {
            return await _context.Tours
                .CountAsync(t => t.CategoryId == categoryId);
        }
        public async Task<bool> IsCategoryNameUniqueAsync(string name)
        {
            return !await _context.Categories
                .AnyAsync(c => c.Name.ToLower() == name.ToLower());
        }
    }
}