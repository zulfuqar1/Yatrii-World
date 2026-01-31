using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Domain.Entities.Base;
namespace YatriiWorld.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T: BaseEntitiy
    {
        // Bütün siyahını gətir
        IQueryable<T> GetAll();

        // ID-yə görə tək bir element gətir
        Task<T> GetByIdAsync(int id);

        // Yeni element əlavə et
        Task AddAsync(T entity);

        // Elementi yenilə
        void Update(T entity);

        // Elementi sil
        void Delete(T entity);

        // Dəyişiklikləri bazada yadda saxla
        Task<int> SaveAsync();
    }

}
