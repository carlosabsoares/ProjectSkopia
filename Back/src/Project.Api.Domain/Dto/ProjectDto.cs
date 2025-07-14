using Project.Api.Domain.Enum;

namespace Project.Api.Domain.Dto
{
    public class ProjectDto 
    {
        //public long Id { get; set; }
        public Guid Uuid { get; init; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

        public string Status { get; set; } = string.Empty;
        public IEnumerable<TaskDto> Tasks { get; set; } = null!;
        //public long AuthorId { get; set; }
        public virtual UserDto Author { get; set; } = null!;
    }
}