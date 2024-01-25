using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repository
{
    public interface IRepository
    {
    }

    public interface IRepository<TEntity> : IRepository where TEntity : class
    {
        IQueryable<TEntity> Get();
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(object entityToUpdate, params object[] id);
        Task<TEntity> DeleteAsync(params object[] id);
        Task<TEntity> GetByIdAsync(params object[] id);
    }
}
