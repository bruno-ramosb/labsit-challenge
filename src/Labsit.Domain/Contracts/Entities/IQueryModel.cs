namespace Labsit.Domain.Contracts.Entities
{
    public interface IQueryModel;

    public interface IQueryModel<out TKey> : IQueryModel where TKey : IEquatable<TKey>
    {
        TKey Id { get; }
    }
}
