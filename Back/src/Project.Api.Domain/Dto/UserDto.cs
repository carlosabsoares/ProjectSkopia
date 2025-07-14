using Project.Api.Domain.Enum;

namespace Project.Api.Domain.Dto
{
    public class UserDto 
    {
        public Guid Uuid { get; init; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public string Name { get; set; } = string.Empty;
        public RoleUserType Role { get; set; } = RoleUserType.User;
        public bool IsActive { get; set; } = true;
    }
}