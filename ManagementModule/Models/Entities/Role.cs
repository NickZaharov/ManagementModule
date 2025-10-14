namespace ManagementModule.Models.Entities
{
        public class Role
        {
            public int Id { get; set; }
            public required string Name { get; set; }

            public List<UserRole> UserRoles { get; set; } = new();
            public List<RolePermission> RolePermissions { get; set; } = new();
        }
}
