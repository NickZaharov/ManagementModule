using ManagementModule.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ManagementModule.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users =>  Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }   
    }
}
