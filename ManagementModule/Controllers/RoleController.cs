using Kendo.Mvc.UI;
using ManagementModule.Models.Entities;
using ManagementModule.Services.Roles;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ManagementModule.DTOs.Roles;

namespace ManagementModule.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public ActionResult Manage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            await _roleService.Create(name);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _roleService.Delete(id))
            {
                return NotFound();
            }
            
            return Ok();
        }
            
        [HttpGet]
        public async Task<JsonResult> GetRoles()
        {
            var result = await _roleService.GetRolesAsync();

            return Json(result);  
        }

        [HttpGet]
        public async Task<JsonResult> GetRolePermissions(string roleId)
        {
            int id;
            if (!int.TryParse(roleId, out id))
            {
                return Json(new { error = "Invalid roleId format." });
            }

            var result = await _roleService.GetRolePermissionsTreeAsync(id);

            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> SaveRolePermissions([FromBody] RolePermissionSaveDto request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            await _roleService.SaveRolePermissionsAsync(request.RoleId, request.PermissionIds ?? new List<int>());
            return Ok();
        }
    }
}
