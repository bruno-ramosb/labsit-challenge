namespace Labsit.Domain.Contracts.Entities
{
    public interface IAuditableEntity<TId> : IAuditableEntity, IEntity<TId>
    {
    }

    public interface IAuditableEntity : IEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}
