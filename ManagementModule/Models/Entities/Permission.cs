namespace ManagementModule.Models.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Category { get; set; }

        public List<RolePermission> RolePermissions { get; set; } = new();
    }
}
