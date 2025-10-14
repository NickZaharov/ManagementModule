using ManagementModule.Models;
using ManagementModule.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementModule.Services.Users
{
    public static class UserQueryExtensions
    {
        public static IQueryable<User> ApplyFilter(this IQueryable<User> query, UserQueryFilter filter, AppDbContext db)
        {
            if (filter.RoleId.HasValue)
            {
                var roleId = filter.RoleId.Value;
                var userIdsWithRole = db.UserRoles
                    .Where(ur => ur.RoleId == roleId)
                    .Select(ur => ur.UserId);

                query = query.Where(u => userIdsWithRole.Contains(u.Id));
            }

            if (filter.PermissionId.HasValue)
            {
                var permissionId = filter.PermissionId.Value;

                var roleIdsWithPermission = db.RolePermissions
                    .Where(rp => rp.PermissionId == permissionId)
                    .Select(rp => rp.RoleId);

                var userIdsWithPermission = db.UserRoles
                    .Where(ur => roleIdsWithPermission.Contains(ur.RoleId))
                    .Select(ur => ur.UserId);

                query = query.Where(u => userIdsWithPermission.Contains(u.Id));
            }

            if (!string.IsNullOrWhiteSpace(filter.SearchName))
            {
                var search = $"%{filter.SearchName}%";
                query = query.Where(u =>
                    EF.Functions.Like(u.FirstName + " " + u.LastName, search));
            }

            return query;
        }

    }
}
