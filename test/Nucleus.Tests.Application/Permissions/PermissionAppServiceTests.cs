﻿using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Nucleus.Application.Permissions;
using Nucleus.Application.Permissions.Dto;
using Nucleus.Core.Permissions;
using Nucleus.Core.Roles;
using Xunit;

namespace Nucleus.Tests.Application.Permissions
{
    public class PermissionAppServiceTests : ApplicationTestBase
    {
        private readonly IPermissionAppService _permissionAppService;

        public PermissionAppServiceTests()
        {
            _permissionAppService = TestServer.Host.Services.GetRequiredService<IPermissionAppService>();
        }

        [Fact]
        public async void Should_Get_Permissions()
        {
            var permissionListInput = new PermissionListInput();
            var permissionList = await _permissionAppService.GetPermissionsAsync(permissionListInput);
            Assert.True(permissionList.Items.Count >= 0);
        }

        [Fact]
        public async void Should_Not_Get_Permissions()
        {
            var permissionListInput = new PermissionListInput { Filter = DefaultPermissions.PermissionNameForAdministration };
            var permissionListEmpty = await _permissionAppService.GetPermissionsAsync(permissionListInput);
            Assert.Equal(1, permissionListEmpty.Items.Count);
        }

        [Fact]
        public async Task Should_Permisson_Granted_To_User()
        {
            var isPermissionGranted =
                await _permissionAppService.IsPermissionGrantedToUserAsync(ContextUser.Identity.Name, DefaultPermissions.MemberAccess.Id);

            Assert.True(isPermissionGranted);
        }

        [Fact]
        public async Task Should_Not_Permisson_Granted_To_User()
        {
            var isPermissionNotGranted =
                await _permissionAppService.IsPermissionGrantedToUserAsync(null, DefaultPermissions.MemberAccess.Id);

            Assert.False(isPermissionNotGranted);
        }

        [Fact]
        public async Task Should_Permisson_Granted_To_Role()
        {
            var isPermissionGranted =
                await _permissionAppService.IsPermissionGrantedToRoleAsync(DefaultRoles.Member, DefaultPermissions.MemberAccess);

            Assert.True(isPermissionGranted);
        }

        [Fact]
        public async Task Should_Not_Permisson_Granted_To_Role()
        {
            var isPermissionNotGranted =
                await _permissionAppService.IsPermissionGrantedToRoleAsync(new Role(), DefaultPermissions.AdministrationAccess);

            Assert.False(isPermissionNotGranted);
        }
    }
}