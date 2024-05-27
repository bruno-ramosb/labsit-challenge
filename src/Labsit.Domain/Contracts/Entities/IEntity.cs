namespace Labsit.Domain.Contracts.Entities
{
    public interface IEntity
    {
    }

    public interface IEntity<TId> : IEntity
    {
        public TId Id { get; set; }
    }
}
