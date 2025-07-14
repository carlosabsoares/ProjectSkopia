using Project.Api.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.Domain.Entities
{
    public class TaskAuditEntity : BaseFullEntity
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

        public long EditorId { get; set; }

        public DateTime Date { get; set; }

        public long TaskId { get; set; }
        public virtual TaskEntity Task { get; set; } = null!;
        public virtual UserEntity Author { get; set; } = null!;
        public virtual ProjectEntity Project { get; set; }
    }
}