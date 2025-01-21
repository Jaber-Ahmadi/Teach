using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using NiazRooz.DomainClasses;
using NiazRooz.DTO;
using NiazRooz.Services;
using NiazRooz.Services.security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace NiazRooz.Web.Areas.Admin.Controllers
{
    [PermissionChecker(2)]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private IUserService _userService;
        private IPermissionService _permissionService;

        public UsersController(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }

        public UserForAdminViewModel userForAdminViewModel { get; set; }

        public IActionResult Index()
        {

            return View();
        }

        [Route("Admin/ListUsers")]
        public IActionResult ListUsers(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            userForAdminViewModel = _userService.GetUsers(pageId, filterEmail, filterUserName);
            return View(userForAdminViewModel);
        }



        [Route("Admin/ListDeleteUsers")]
        public IActionResult ListDeleteUsers(int pageId = 1, string filterUserName = "", string filterEmail = "")
        {
            userForAdminViewModel = _userService.GetDeleteUsers(pageId, filterEmail, filterUserName);
            return View(userForAdminViewModel);
        }


        [Route("Admin/CreateUsers")]
        [HttpGet]
        public IActionResult CreateUsers()
        {
            var createUserViewModel = new CreateUserViewModel { Roles = _permissionService.GetRoles() };
            return View(createUserViewModel);
        }
        [Route("Admin/CreateUsers")]
        [HttpPost]
        public IActionResult CreateUsers(List<int> SelectedRoles, CreateUserViewModel createUser)
        {
            if (!ModelState.IsValid)
            {
                createUser.Roles = _permissionService.GetRoles();
                return View(createUser);
            }


            int userId = _userService.AddUserFromAdmin(createUser);

            //Add Roles
            _permissionService.AddRolesToUser(SelectedRoles, userId);

            return Redirect("ListUsers");
        }


        [HttpGet]
        public IActionResult EditUsers(int id)
        {

            var editUserViewModel = _userService.GetUserForShowInEditMode(id);
            editUserViewModel.Roles = _permissionService.GetRoles();
            return View(editUserViewModel);
        }
        [HttpPost]

        public IActionResult EditUsers(List<int> SelectedRoles, EditUserViewModel editUser)
        {
            if (!ModelState.IsValid)
            {
                 editUser = _userService.GetUserForShowInEditMode(editUser.UserId);
                editUser.Roles = _permissionService.GetRoles();
                return View(editUser);
            }

            _userService.EditUserFromAdmin(editUser);

            //Edit Roles
            _permissionService.EditRolesUser(editUser.UserId, SelectedRoles);
            return RedirectToAction("ListUsers");
        }


        [HttpGet]
        public IActionResult DeleteUsers(int id)
        {
            ViewData["UserId"] = id;
            var informationUserViewModel = _userService.GetUserInformationById(id);
            return View(informationUserViewModel);
        }


        [HttpPost]
        public IActionResult DeleteUsers(int UserId, string UserName)
        {
            _userService.DeleteUser(UserId);
            return RedirectToAction("ListUsers");
        }


        public IActionResult SearcheUserforShowPage(int pageId, UserForAdminViewModel userForAdmin)
        {
            string filterEmail = userForAdmin.filterEmail;
            string filterUserName = userForAdmin.filterUserName;
            userForAdmin = _userService.GetUsersForFilter(pageId, filterEmail, filterUserName);
            return View(userForAdmin);
        }


        public IActionResult UndoUsers(int id)
        {

            _userService.UndoUsersByAdmin(id);
            return RedirectToAction("ListDeleteUsers");
        }

    }
}