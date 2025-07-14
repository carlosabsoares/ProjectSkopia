using Project.Api.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.Domain.Entities
{
    public class TaskEntity : BaseFullEntity
    {
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        [MinLength(3)]
        public string Description { get; set; } = string.Empty;

        public DateTime ExpirationDate { get; set; }
        public StatusTaskType Status { get; set; } = StatusTaskType.Pending;

        public long AuthorId { get; set; }

        public long ProjectId { get; set; }
        public virtual ProjectEntity Project { get; set; } = null!;
        public virtual UserEntity Author { get; set; } = null!;
    }
}