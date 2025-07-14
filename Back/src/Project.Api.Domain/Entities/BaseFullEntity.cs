namespace Project.Api.Domain.Entities
{
    public abstract class BaseFullEntity : BaseEntity
    {
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}