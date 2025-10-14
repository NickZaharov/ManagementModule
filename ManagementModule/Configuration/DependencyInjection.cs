using ManagementModule.Models;
using ManagementModule.Services.Roles;
using ManagementModule.Services.Users;
using Microsoft.EntityFrameworkCore;

namespace Pharmacy.Web.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration config)
        {

            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            return services;
        }
    }
}
