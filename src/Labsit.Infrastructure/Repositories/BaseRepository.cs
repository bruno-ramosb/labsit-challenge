﻿using Labsit.Domain.Contracts.Entities;
using Labsit.Domain.Contracts.Repositories;
using Labsit.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Labsit.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity, Tkey>(LabsitContext context) : IBaseRepository<TEntity, Tkey>
    where TEntity : class, IEntity<Tkey>
    where Tkey : IEquatable<Tkey>
    {
        private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
        protected readonly LabsitContext Context = context;

        public async Task Add(TEntity entity) => 
            await _dbSet.AddAsync(entity);

        public Task Update(TEntity entity) =>
            Task.FromResult(_dbSet.Update(entity));

        public Task Remove(TEntity entity) =>
            Task.FromResult(_dbSet.Remove(entity));

        public async Task<TEntity> GetByIdAsync(Tkey id) =>
            await _dbSet.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(entity => entity.Id.Equals(id));

        #region IDisposable

        private bool _disposed;

        ~BaseRepository() => Dispose(false);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                Context.Dispose();

            _disposed = true;
        }

        #endregion
    }
}
