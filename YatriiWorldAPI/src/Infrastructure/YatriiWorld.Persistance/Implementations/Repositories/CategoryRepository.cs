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

        // 1. İçinde en az bir aktif tur olan kategorileri getirir
        public async Task<List<Category>> GetCategoriesWithActiveToursAsync()
        {
            return await _context.Categories
                .Where(c => c.Tours.Any(t => t.IsDeleted)) // Sadece "dolu" ve "aktif" kategoriler
                .Include(c => c.Tours.Where(t => t.IsDeleted)) // Sadece aktif turları içine al
                .AsNoTracking()
                .ToListAsync();
        }

        // 2. Tüm kategorileri turlarıyla beraber listeler
        public async Task<IEnumerable<Category>> GetCategoriesWithToursAsync()
        {
            return await _context.Categories
                .Include(c => c.Tours)
                .AsNoTracking()
                .ToListAsync();
        }

        // 3. Kategoriyi turları ve turların tüm detaylarıyla (resim/tag) beraber getirir
        public async Task<IEnumerable<Category>> GetCategoryWithDetailsAsync()
        {
            return await _context.Categories
                .Include(c => c.Tours)
                    .ThenInclude(t => t.Images) // Tour içindeki görseller
                .Include(c => c.Tours)
                    .ThenInclude(t => t.Tags)   // Tour içindeki etiketler
                .AsNoTracking()
                .ToListAsync();
        }

        // 4. Spesifik bir kategoriyi ID ile ve turlarıyla getirir
        public async Task<Category> GetCategoryWithToursByIdAsync(long id)
        {
            return await _context.Categories
                .Include(c => c.Tours)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        // 5. Veritabanına sormadan hızlıca tur sayısını döner
        public async Task<int> GetTotalTourCountAsync(long categoryId)
        {
            return await _context.Tours
                .CountAsync(t => t.CategoryId == categoryId);
        }

        // 6. BaseNameableEntity'den gelen 'Name' alanını kontrol eder
        public async Task<bool> IsCategoryNameUniqueAsync(string name)
        {
            // İsim çakışmasını engellemek için (Case-insensitive)
            return !await _context.Categories
                .AnyAsync(c => c.Name.ToLower() == name.ToLower());
        }
    }
}