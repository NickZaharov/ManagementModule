using ManagementModule.DTOs.Users;
using ManagementModule.Models.Entities;

namespace ManagementModule.Services.Users
{
    public interface IUserService
    {
        Task<IReadOnlyList<User>> GetUsersAsync(UserQueryFilter filter, CancellationToken ct);

        Task<User?> FindUserByIdAsync(int id);

        Task<UserDetailsViewDto?> GetUserDetailsByIdAsync(int id, CancellationToken ct);

        Task<bool> AssignRolesAsync(int id, List<int> AssignedRoles, CancellationToken ct);
    }
}
