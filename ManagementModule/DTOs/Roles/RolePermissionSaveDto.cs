namespace ManagementModule.DTOs.Roles
{
    public class RolePermissionSaveDto
    {
        public int RoleId { get; set; }
        public List<int> PermissionIds { get; set; } = new();
    }
}
