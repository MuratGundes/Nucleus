﻿using System.Linq;
using Nucleus.Core.Permissions;
using Nucleus.Core.Roles;
using Nucleus.Core.Users;

namespace Nucleus.EntityFramework
{
    public class SeedData
    {
        #region BuildData
        public static User[] BuildApplicationUsers()
        {
            return DefaultUsers.All().ToArray();
        }

        public static Role[] BuildApplicationRoles()
        {
            return DefaultRoles.All().ToArray();
        }

        public static UserRole[] BuildApplicationUserRoles()
        {
            return new[]
            {
                new UserRole
                {
                    RoleId = DefaultRoles.AdminRole.Id,
                    UserId = DefaultUsers.AdminUser.Id
                },
                new UserRole
                {
                    RoleId = DefaultRoles.Member.Id,
                    UserId = DefaultUsers.ApiUser.Id
                }
            };
        }

        public static Permission[] BuildPermissions()
        {
            return DefaultPermissions.All().ToArray();
        }

        public static RolePermission[] BuildRolePermissions()
        {
            var rolePermissions = DefaultPermissions.All().Select(p =>
                new RolePermission
                {
                    PermissionId = p.Id,
                    RoleId = DefaultRoles.AdminRole.Id
                }).ToList();

            rolePermissions.Add(new RolePermission
            {
                PermissionId = DefaultPermissions.ApiAccess.Id,
                RoleId = DefaultRoles.Member.Id
            });

            return rolePermissions.ToArray();
        }
        #endregion
    }
}