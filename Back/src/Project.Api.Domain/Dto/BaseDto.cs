namespace Project.Api.Domain.Dto
{
    public class BaseDto
    {
        public long Id { get; set; }
        public Guid Uuid { get; init; }
    }
}