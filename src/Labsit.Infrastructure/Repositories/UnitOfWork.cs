using Labsit.Domain.Contracts.Repositories;
using Labsit.Infrastructure.Context;

namespace Labsit.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LabsitContext _dbContext;
        private bool disposed;

        public UnitOfWork(LabsitContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void BeginTransaction()
        {
            _dbContext.Database.BeginTransaction();
        }

        public async Task<int> Commit(CancellationToken cancellationToken) => 
            await _dbContext.SaveChangesAsync(cancellationToken);
        

        public void Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
                _dbContext.Dispose();

            disposed = true;
        }


    }
}
