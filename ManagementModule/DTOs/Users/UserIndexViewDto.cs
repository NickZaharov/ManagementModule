using ManagementModule.DTOs.Roles;
using ManagementModule.Models.Entities;
using ManagementModule.Services.Users;

namespace ManagementModule.DTOs.Users
{
    public class UserIndexViewDto
    {
        public IReadOnlyList<User> Users { get; set; } = null!;
        public IReadOnlyList<RoleDto> Roles { get; set; } = null!;
        public IReadOnlyList<PermissionDto> Permissions { get; set; } = null!;
        public UserQueryFilter Filter { get; set; } = new();
    }
}

