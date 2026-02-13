using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YatriiWorld.Application.Interfaces.Repositories;
using YatriiWorld.Domain.Entities.Base;
using YatriiWorld.Persistance.Data;

namespace YatriiWorld.Persistance.Implementations.Repositories.Generic
{
    internal class Repository<T> : IRepository<T> where T : BaseEntitiy, new()
    {
        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
