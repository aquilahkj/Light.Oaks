using System;
namespace Light.Oaks
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPermissionModule
    {
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //RolePermission[] GetRolePermissions();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Role[] GetRoles();
    }
}
