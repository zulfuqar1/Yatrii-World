using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Application.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductWithDetailsAsync(long id);

    }
}