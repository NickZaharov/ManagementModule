namespace ManagementModule.DTOs.Roles
{
    public class RolePermissionDto
    {
        public int RoleId { get; init; }
        public string RoleName { get; init; } = string.Empty;
        public List<string> Permissions { get; init; } = new();
    }
}
