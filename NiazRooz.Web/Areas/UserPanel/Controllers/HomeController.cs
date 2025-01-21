using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NiazRooz.Services;
using NiazRooz.DTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace NiazRooz.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View(_userService.GetUserInformationByUserName(User.Identity.Name));
        }

        [Route("EditProfile")]
        public IActionResult EditProfile()
        {
            return View(_userService.GetDataForEditProfileUser(User.Identity.Name));
        }

        [Route("EditProfile")]
        [HttpPost]
        public IActionResult EditProfile(EditProfileViewModel profile)
        {
            if (!ModelState.IsValid)
                return View(profile);

            _userService.EditProfile(User.Identity.Name, profile);

            //Log Out User
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/Login?EditProfile=true");

        }

        [Route("ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }


        [Route("ChangePassword")]
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel change)
        {
            string currentUserName = User.Identity.Name;

            if (!ModelState.IsValid)
                return View(change);

            if (!_userService.CompareOldPassword(change.OldPassword, currentUserName))
            {
                ModelState.AddModelError("OldPassword", "کلمه عبور فعلی صحیح نمیباشد");
                return View(change);
            }

            _userService.ChangeUserPassword(currentUserName, change.Password);
            ViewBag.IsSuccess = true;

            return Redirect("/Login?ChangePassword=true");
        }

        [Route("ChangeProfileImage")]
        public IActionResult ChangeProfileImage()
        {
            return View(_userService.GetDataForEditProfileUser(User.Identity.Name));

        }
        [Route("ChangeProfileImage")]
        [HttpPost]
        public IActionResult ChangeProfileImage(EditProfileViewModel profile)
        {
            if (!ModelState.IsValid)
                return View(profile);

            _userService.EditProfile(User.Identity.Name, profile);
            return View(_userService.GetDataForEditProfileUser(User.Identity.Name));
        }

    }
}