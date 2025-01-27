﻿using System;
using System.Collections.Generic;
using System.Text;
using NiazRooz.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace NiazRooz.Services.security
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private IPermissionService _permissionService;
        private int _permissionId = 0;
        public PermissionCheckerAttribute(int permissionId)
        {
            _permissionId = permissionId;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _permissionService =
                (IPermissionService) context.HttpContext.RequestServices
                    .GetService(typeof(IPermissionService));

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string userName = context.HttpContext.User.Identity.Name;

                if (!_permissionService.CheckPermission(_permissionId, userName))
                {
                    context.Result = new RedirectResult("/Login");
                }
            }
            else
            {
                context.Result = new RedirectResult("/Login");
            }
        }
    }
}
