using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbContext _context;
        protected DbSet<TEntity> _set;

        public Repository(DbContext context)
        {
            _context = context;
            _set = _context.Set<TEntity>();
        }


        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var result = await _set.AddAsync(entity);
            return result.Entity;
        }

        public async Task<TEntity> DeleteAsync(params object[] id)
        {
            TEntity entityToDelete = await GetByIdAsync(id);

            if (entityToDelete != null)
            {
                _set.Remove(entityToDelete);
            }

            return entityToDelete;
        }

        public IQueryable<TEntity> Get()
        {
            return _set;
        }

        public async Task<TEntity> GetByIdAsync(params object[] id)
        {
            return  await _set.FindAsync(id);
        }

        public async Task<TEntity> UpdateAsync(object entityToUpdate, params object[] id)
        {
            TEntity targetState = await GetByIdAsync(id);

            if (targetState == null)
                throw new KeyNotFoundException($"There are no records with id: {id}");

            _context.Entry(targetState).CurrentValues.SetValues(entityToUpdate);

            return targetState;
        }
    }
}
