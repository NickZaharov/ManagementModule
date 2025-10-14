using ManagementModule.DTOs.Roles;
using ManagementModule.Models.Entities;

namespace ManagementModule.Services.Roles
{
    public interface IRoleService
    {
        Task Create(string name);
        Task<bool> Delete(int id);
        Task<IReadOnlyList<RoleDto>> GetRolesAsync();
        Task<List<TreeViewItemDto>> GetRolePermissionsTreeAsync(int id);
        Task SaveRolePermissionsAsync(int roleId, IEnumerable<int> permissionIds);
        Task<IReadOnlyList<PermissionDto>> GetAllPermissionsAsync();
    }
}
