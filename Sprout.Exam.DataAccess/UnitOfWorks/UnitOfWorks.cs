using Microsoft.EntityFrameworkCore;
using Sprout.Exam.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.UnitOfWorks
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext, new()
    {
        private readonly Dictionary<Type, object> _repositoryDictionary = new Dictionary<Type, object>();

        protected TContext _context;
        protected bool disposed;

        public UnitOfWork(TContext context)
        {
            _context = context;

            if (context == null)
            {
                throw new InvalidOperationException("Entity.DbContext instance is expected as a dbContext parameter.");
            }
        }

        public IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class
        {
            if (_repositoryDictionary.TryGetValue(typeof(TEntity), out var value))
            {
                return value as IRepository<TEntity>;
            }
            var repo = new Repository<TEntity>(_context);
            _repositoryDictionary.Add(typeof(TEntity), repo);
            return repo;
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing && _context is IDisposable context)
            {
                context.Dispose();
            }
            disposed = true;
        }
    }
}
