namespace Project.Api.Domain.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public Guid Uuid { get; init; } = Guid.NewGuid();
    }
}