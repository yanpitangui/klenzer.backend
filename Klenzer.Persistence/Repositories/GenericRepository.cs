
using Klenzer.Domain.Entities;
using Klenzer.Domain.Repositories;
using Klenzer.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Klenzer.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly KlenzerDbContext _dbContext;

        public GenericRepository(KlenzerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

      
        public async Task<TEntity> GetById(Guid id)
        {
            return await _dbContext.Set<TEntity>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public async Task<TEntity> Update(Guid id, TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return entity;
        }

        public async Task<TEntity> Delete(Guid id)
        {
            var entity = await GetById(id);
            _dbContext.Set<TEntity>().Remove(entity);
            return entity;

        }

        public async Task<int> Commit()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
