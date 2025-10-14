using ManagementModule.DTOs.Roles;

namespace ManagementModule.DTOs.Users
{
    public class UserDto
    {
        public int Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public List<RolePermissionDto> Roles { get; init; } = new();
    }
}
