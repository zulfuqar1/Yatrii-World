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
        Task<IEnumerable<Tour>> GetToursWithDetailsAsync();//standart All tours with details

        Task<List<Tour>> GetFilteredToursAsync(string search, decimal? minPrice, decimal? maxPrice);//price search

        Task<List<Tour>> GetPagedToursAsync(int pageNumber, int pageSize);//limitedpagination

        Task<List<Tour>> GetTopRatedToursAsync(int count);//specially top rated

        Task<int> GetTourCountByCategoryAsync(long categoryId);//category......

        Task<Tour> GetTourByIdWithDetailsAsync(long id);



    }
}
