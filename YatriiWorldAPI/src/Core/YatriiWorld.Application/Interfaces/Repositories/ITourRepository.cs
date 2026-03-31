using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Domain.Entities;

namespace YatriiWorld.Application.Interfaces.Repositories
{
    public interface ITourRepository : IRepository<Tour>
    {
        Task<IEnumerable<Tour>> GetToursWithDetailsAsync();

        Task<List<Tour>> GetFilteredToursAsync(string search, decimal? minPrice, decimal? maxPrice);

        Task<List<Tour>> GetPagedToursAsync(int pageNumber, int pageSize);

        Task<List<Tour>> GetTopRatedToursAsync(int count);

        Task<int> GetTourCountByCategoryAsync(long categoryId);

        Task<Tour> GetTourByIdWithDetailsAsync(long id);



    }
}
