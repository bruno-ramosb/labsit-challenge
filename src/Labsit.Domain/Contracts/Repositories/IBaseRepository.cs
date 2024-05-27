using Labsit.Domain.Contracts.Entities;

namespace Labsit.Infrastructure.Repositories
{
    public interface IBaseRepository<TEntity, in TKey> : IDisposable
    where TEntity : IEntity<TKey>
    where TKey : IEquatable<TKey>
    {
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(TEntity entity);
        Task<TEntity> GetByIdAsync(TKey id);
    }
}
