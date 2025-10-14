using ManagementModule.Services.Users;
using ManagementModule.Services.Roles;
using Microsoft.AspNetCore.Mvc;
using ManagementModule.DTOs.Users;

namespace ManagementModule.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UserController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(UserQueryFilter filter, CancellationToken ct)
        {
            var users = await _userService.GetUsersAsync(filter, ct);
            var roles = await _roleService.GetRolesAsync();
            var permissions = await _roleService.GetAllPermissionsAsync();

            var vm = new UserIndexViewDto
            {
                Users = users,
                Roles = roles,
                Permissions = permissions,
                Filter = filter
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, CancellationToken ct)
        {
            var result = await _userService.GetUserDetailsByIdAsync(id, ct);

            if (result == null) return NotFound();

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoles(int id, List<int> AssignedRoles, CancellationToken ct)
        {
            if(_userService.FindUserByIdAsync(id) == null) return NotFound();

            await _userService.AssignRolesAsync(id, AssignedRoles, ct);

            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
