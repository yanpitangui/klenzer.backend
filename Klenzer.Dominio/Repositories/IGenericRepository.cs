using Klenzer.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Klenzer.Domain.Repositories
{
    public interface IGenericRepository<TEntity>
     where TEntity : BaseEntity
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(Guid id);

        Task<TEntity> Create(TEntity entity);

        Task<TEntity> Update(Guid id, TEntity entity);

        Task<TEntity> Delete(Guid id);

        Task<int> Commit();
    }
}
