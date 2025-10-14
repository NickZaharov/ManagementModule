namespace ManagementModule.Models.Entities
{
    public class RolePermission
    {
        public int Id { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; } = null!;

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}
