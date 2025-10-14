using Kendo.Mvc.Extensions;
using ManagementModule.DTOs.Roles;
using ManagementModule.DTOs.Users;
using ManagementModule.Models;
using ManagementModule.Models.Entities;
using ManagementModule.Models.Kendo;
using Microsoft.EntityFrameworkCore;

namespace ManagementModule.Services.Users
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User?> FindUserByIdAsync(int id) => await _db.Users.FindAsync(id);

        public async Task<IReadOnlyList<User>> GetUsersAsync(UserQueryFilter filter, CancellationToken ct)
        {
            var query = _db.Users.AsNoTracking();

            query = query.ApplyFilter(filter, _db);

            return await query.ToListAsync(ct);
        }

        public async Task<UserDetailsViewDto?> GetUserDetailsByIdAsync(int userId, CancellationToken ct)
        {
            var dto = await _db.Users
                .Where(u => u.Id == userId)
                .Select(u => new UserDetailsViewDto
                {
                    UserId = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    SelectedRoleIds = u.UserRoles.Select(ur => ur.RoleId).ToList(),
                })
                .FirstOrDefaultAsync(ct);

            if (dto is null)
                return null;

            dto.AvailableRoles = await _db.Roles
                .Select(r => new InputGroupItemModel
                {
                    Label = r.Name,
                    Value = r.Id.ToString(),
                    Enabled = true,
                    Encoded = false
                })
                .ToListAsync(ct);

            dto.PermissionsByCategory = await _db.UserRoles
                .Where(ur => ur.UserId == userId)
                .SelectMany(ur => ur.Role.RolePermissions)
                .Select(rp => rp.Permission)
                .GroupBy(p => p.Category)
                .Select(g => new PermissionsByCategoryDto
                {
                    Category = g.Key,
                    Permissions = g.Select(p => p.Name).Distinct().ToList()
                }).ToListAsync(ct);

            return dto;
        }

        public async Task<bool> AssignRolesAsync(int userId, List<int> AssignedRoles, CancellationToken ct)
        {
            var userRoles = await _db.UserRoles
                .Where(ur => ur.UserId == userId)
                .ToListAsync(ct);

            var currentRoleIds = userRoles.Select(ur => ur.RoleId).ToHashSet();
            var incomingRoleIds = AssignedRoles.ToHashSet();

            var rolesToAdd = incomingRoleIds.Except(currentRoleIds);
            var rolesToRemove = currentRoleIds.Except(incomingRoleIds);

            var newRoles = rolesToAdd.Select(roleId => new UserRole
            {
                UserId = userId,
                RoleId = roleId
            }).ToList();

            var rolesToDelete = userRoles
                .Where(ur => rolesToRemove.Contains(ur.RoleId))
                .ToList();

            if (!newRoles.Any() && !rolesToDelete.Any())
                return false;

            if (newRoles.Any())
                _db.UserRoles.AddRange(newRoles);

            if (rolesToDelete.Any())
                _db.UserRoles.RemoveRange(rolesToDelete);

            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
