using Project.Api.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.Domain.Entities
{
    public class ProjectEntity : BaseFullEntity
    {
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Description { get; set; }

        public StatusProjectType Status { get; set; } = StatusProjectType.Active;
        public IEnumerable<TaskEntity> Tasks { get; set; } = null!;
        public long AuthorId { get; set; }
        public virtual UserEntity Author { get; set; } = null!;
    }
}