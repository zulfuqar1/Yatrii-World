using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using YatriiWorld.Application.Interfaces.Repositories;
using YatriiWorld.Domain.Entities;
using YatriiWorld.Persistance.Data;
using YatriiWorld.Persistance.Implementations.Repositories.Generic;

namespace YatriiWorld.Persistance.Implementations.Repositories
{
 
    internal class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public async Task<Product> GetProductWithDetailsAsync(long id)
        {
            return await _context.Products
                .Include(p => p.Images)
                .Include(p => p.Category)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }
    }
}