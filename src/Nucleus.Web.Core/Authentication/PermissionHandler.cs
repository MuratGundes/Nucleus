﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Nucleus.Application.Permissions;

namespace Nucleus.Web.Core.Authentication
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionAppService _permissionApp;

        public PermissionHandler(IPermissionAppService permissionApp)
        {
            _permissionApp = permissionApp;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                context.Fail();
                return;
            }

            var hasPermission = await _permissionApp.IsPermissionGrantedToUserAsync(context.User.Identity.Name, requirement.Permission.Id);
            if (hasPermission)
            {
                context.Succeed(requirement);
            }
        }
    }
}
