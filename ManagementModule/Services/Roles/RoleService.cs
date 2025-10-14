using ManagementModule.DTOs.Roles;
using ManagementModule.Models;
using ManagementModule.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace ManagementModule.Services.Roles
{
    public class RoleService : IRoleService
    {
        private readonly AppDbContext _db;

        public RoleService(AppDbContext db)
        {
            _db = db;
        }

        public async Task Create(string name)
        {
            _db.Roles.Add(new Role { Name = name });
            await _db.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            var role = await _db.Roles.FindAsync(id);
            if (role == null) return false;

            _db.Roles.Remove(role);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<IReadOnlyList<RoleDto>> GetRolesAsync()
        {
            return await _db.Roles
                .Select(role => new RoleDto
                {
                    RoleName = role.Name,
                    RoleId = role.Id,
                })
                .ToListAsync();
        }

        public async Task<List<TreeViewItemDto>> GetRolePermissionsTreeAsync(int roleId)
        {
            var rolePermissionsSet = await _db.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .Select(rp => rp.PermissionId)
                .ToHashSetAsync();

            var permissions = await _db.Permissions
                .ToListAsync();

            var grouped = permissions
                .GroupBy(p => p.Category)
                .ToDictionary(g => g.Key, g => g.ToList());

            var result = new List<TreeViewItemDto>();

            foreach (var group in grouped)
            {
                var groupNode = new TreeViewItemDto
                {
                    Text = group.Key,
                    Expanded = false
                };

                foreach (var permission in group.Value)
                {
                    groupNode.Items.Add(new TreeViewItemDto
                    {
                        Id = permission.Id,
                        Text = permission.Name,
                        Checked = rolePermissionsSet.Contains(permission.Id)
                    });
                }

                if (groupNode.Items.Count > 0 && groupNode.Items.All(i => i.Checked == true))
                {
                    groupNode.Checked = true;
                }

                result.Add(groupNode);
            }

            return result;
        }

        public async Task SaveRolePermissionsAsync(int roleId, IEnumerable<int> permissionIds)
        {
            var existing = _db.RolePermissions.Where(rp => rp.RoleId == roleId);
            _db.RolePermissions.RemoveRange(existing);

            if (permissionIds != null)
            {
                foreach (var pid in permissionIds.Distinct())
                {
                    _db.RolePermissions.Add(new RolePermission
                    {
                        RoleId = roleId,
                        PermissionId = pid
                    });
                }
            }

            await _db.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<PermissionDto>> GetAllPermissionsAsync()
        {
            return await _db.Permissions
                .OrderBy(p => p.Name)
                .Select(p => new PermissionDto
                {
                    PermissionId = p.Id,
                    PermissionName = p.Name
                })
                .ToListAsync();
        }
    }
}
