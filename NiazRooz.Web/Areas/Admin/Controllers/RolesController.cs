using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NiazRooz.DomainClasses;
using NiazRooz.Services;
using NiazRooz.Services.security;
using Microsoft.AspNetCore.Mvc;

namespace NiazRooz.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker(2)]
    public class RolesController : Controller
    {
        private IPermissionService _permissionService;
        [BindProperty]
        public Role role { get; set; }
        public RolesController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        public IActionResult Index()
        {
            var rolelList= _permissionService.GetRoles();
            return View(rolelList);
        }


        [HttpGet]
        public IActionResult CreateRoles()
        {
            ViewData["Permissions"] = _permissionService.GetAllPermission();
            return View(role);
        }
        [HttpPost]
        public IActionResult CreateRoles(List<int> SelectedPermission)
        {
            if (!ModelState.IsValid)
                return View();

            role.IsDelete = false;
            int roleId = _permissionService.AddRole(role);

            _permissionService.AddPermissionsToRole(roleId, SelectedPermission);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult DeleteRoles(int id)
        {
            ViewData["RoleId"] = id;
            role = _permissionService.GetRoleById(id);
            return View(role);
        }
        [HttpPost]
        public IActionResult DeleteRoles(int RoleId,string RoleTitle)
        {
            var roles = _permissionService.GetRoleById(roleId: RoleId);
            _permissionService.DeleteRole(roles);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditRoles(int id)
        {
            ViewData["Permissions"] = _permissionService.GetAllPermission();
            ViewData["SelectedPermissions"] = _permissionService.PermissionsRole(id);
            role = _permissionService.GetRoleById(id);

            return View(role); 
        }
        [HttpPost]
        public IActionResult EditRoles(List<int> SelectedPermission,Role roles)
        {
            if (!ModelState.IsValid)
                return View();
            _permissionService.UpdateRole(roles);

            _permissionService.UpdatePermissionsRole(roles.RoleId, SelectedPermission);

            return RedirectToAction("Index");
        }
    }
}