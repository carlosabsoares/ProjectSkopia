using Project.Api.Domain.Enum;

namespace Project.Api.Domain.Dto
{
    public class TaskDto
    {
        public Guid Uuid { get; init; }
        public DateTime CreateAt { get; set; } = DateTime.Now;

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DateTime ExpirationDate { get; set; }
        public StatusTaskType Status { get; set; } = StatusTaskType.Pending;

        public virtual UserDto Author { get; set; } = null!;

        public virtual ProjectDto Project { get; set; }
    }
}