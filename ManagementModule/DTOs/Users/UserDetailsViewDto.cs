using Kendo.Mvc.UI;
using ManagementModule.Models.Kendo;

namespace ManagementModule.DTOs.Users
{
    public class UserDetailsViewDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public List<InputGroupItemModel> AvailableRoles { get; set; } = null!;
        public List<int> SelectedRoleIds { get; set; } = null!;

        public List<PermissionsByCategoryDto> PermissionsByCategory { get; set; } = null!;
    }

    public class PermissionsByCategoryDto
    {
        public string Category { get; set; } = string.Empty;
        public List<string> Permissions { get; set; } = null!;
    }
}
