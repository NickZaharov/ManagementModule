using ManagementModule.Models;
using ManagementModule.Models.Entities;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Users.Any()) return;

        // 1. Сотрудники
        var users = new List<User>
        {
            new() { Id = 1, FirstName = "Sam", LastName = "Vinchester" },
            new() { Id = 2, FirstName = "Ann", LastName = "Shirli" },
            new() { Id = 3, FirstName = "Alex", LastName = "Sidorov" },
            new() { Id = 4, FirstName = "Diana", LastName = "Smirnova" },
            new() { Id = 5, FirstName = "Eugene", LastName = "Kuznetsov" },
            new() { Id = 6, FirstName = "Pavel", LastName = "Zaicev" },
            new() { Id = 7, FirstName = "Fill", LastName = "Conors" },
            new() { Id = 8, FirstName = "Alex", LastName = "Sidorov" },
            new() { Id = 9, FirstName = "Diana", LastName = "Smirnova" },
            new() { Id = 10, FirstName = "Eugene", LastName = "Kuznetsov" }
        };

        // 2. Роли
        var roles = new List<Role>
        {
            new() { Id = 1, Name = "Admin" },
            new() { Id = 2, Name = "Editor" },
            new() { Id = 3, Name = "Viewer" },
            new() { Id = 4, Name = "Auditor" },
            new() { Id = 5, Name = "Support" }
        };

        // 3. Права
        var categories = new[] { "User Management", "Content Editing", "Reporting", "System Settings", "Security" };
        var permissions = Enumerable.Range(1, 200).Select(i =>
            new Permission
            {
                Id = i,
                Name = $"Permission {i}",
                Category = categories[i % categories.Length]
            }).ToList();

        // 4. Связи RolePermission (случайные)
        var rolePermissions = new List<RolePermission>();
        var rand = new Random();
        int rpId = 1;

        foreach (var role in roles)
        {
            var assigned = permissions.OrderBy(_ => rand.Next()).Take(20).ToList();
            foreach (var perm in assigned)
            {
                rolePermissions.Add(new RolePermission
                {
                    Id = rpId++,
                    Role = role,
                    Permission = perm,
                    RoleId = role.Id,
                    PermissionId = perm.Id
                });
            }
        }

        // 5. Связи UserRole
        var userRoles = new List<UserRole>();
        int urId = 1;

        foreach (var user in users)
        {
            var assignedRoles = roles.OrderBy(_ => rand.Next()).Take(2).ToList();
            foreach (var role in assignedRoles)
            {
                userRoles.Add(new UserRole
                {
                    Id = urId++,
                    User = user,
                    Role = role,
                    UserId = user.Id,
                    RoleId = role.Id
                });
            }
        }

        // 6. Добавление в контекст
        context.Users.AddRange(users);
        context.Roles.AddRange(roles);
        context.Permissions.AddRange(permissions);
        context.RolePermissions.AddRange(rolePermissions);
        context.UserRoles.AddRange(userRoles);

        context.SaveChanges();
    }
}
