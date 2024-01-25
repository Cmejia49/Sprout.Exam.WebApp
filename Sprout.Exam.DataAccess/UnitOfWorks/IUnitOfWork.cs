using Microsoft.EntityFrameworkCore;
using Sprout.Exam.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.UnitOfWorks
{
    public interface IUnitOfWork<out TContext> : IDisposable where TContext : DbContext, new()
    {
        IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class;
        Task<int> SaveAsync();
    }
}
