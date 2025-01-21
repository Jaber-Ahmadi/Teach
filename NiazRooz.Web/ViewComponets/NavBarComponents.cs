using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NiazRooz.Services;
using NiazRooz.Services.security;


namespace NiazRooz.Web.ViewComponets
{
    public class NavBarComponents:ViewComponent
    {
        private IUserService Users;
        
        public NavBarComponents(IUserService user)
        {
            Users = user;
        }

        //InvokeAsync
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            return await Task.FromResult((IViewComponentResult) View("_NavBars",Users.GetUserAvatarByUserName(User.Identity.Name)));
        }
    }
}
