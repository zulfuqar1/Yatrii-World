using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Domain.Entities.Base;
namespace YatriiWorld.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T: BaseEntity
    {
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(long id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<int> SaveAsync();
    }

}
 