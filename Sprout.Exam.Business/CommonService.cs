using Sprout.Exam.DataAccess;
using Sprout.Exam.DataAccess.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Common
{
    public abstract class CommonService : IDisposable
    {
        protected IUnitOfWork<SproutExamDbContext> UnitOfWork { get; private set; }

        protected CommonService(IUnitOfWork<SproutExamDbContext> unitOfWork)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitOfWork.Dispose();
            }
        }
    }
}
