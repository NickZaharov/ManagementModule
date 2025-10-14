namespace ManagementModule.Services.Users
{
    public class UserQueryFilter
    {
        public string? SearchName { get; set; }
        public int? RoleId { get; set; }
        public int? PermissionId { get; set; }
    }
}
