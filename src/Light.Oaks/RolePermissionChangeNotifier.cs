using System;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Light.Oaks
{
    /// <summary>
    /// 
    /// </summary>
    public class RolePermissionChangeNotifier
    {
        readonly IPermissionModule permissionModule;
        readonly PermissionManagement permissionManagement;

        public RolePermissionChangeNotifier(IServiceProvider serviceProvider)
        {
            permissionModule = serviceProvider.GetService<IPermissionModule>();
            permissionManagement = serviceProvider.GetService<PermissionManagement>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ResetRolePermission()
        {
            if (permissionModule == null) {
                return false;
            }
            permissionManagement.SetRoles(permissionModule.GetRoles());
            return true;
        }
    }
}
