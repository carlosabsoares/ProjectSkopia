using Project.Api.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.Domain.Entities
{
    public class UserEntity : BaseFullEntity
    {
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;

        public RoleUserType Role { get; set; } = RoleUserType.User;
        public bool IsActive { get; set; } = true;
    }
}