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
        protected readonly DbSet<T> _dbSet;
        protected readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
