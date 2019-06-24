using System;
namespace Light.Oaks
{
    /// <summary>
    /// 
    /// </summary>
    public class BasicPermissionModule : IPermissionModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RolePermission[] GetRolePermissions()
        {
            return new RolePermission[] {
                new RolePermission() {
                    Role = "admin",
                    PermissionCode = "admin"
                },
                new RolePermission() {
                    Role = "user",
                    PermissionCode = "create"
                },
                new RolePermission() {
                    Role = "user",
                    PermissionCode = "update"
                },
                new RolePermission() {
                    Role = "user",
                    PermissionCode = "delete"
                },
                new RolePermission() {
                    Role = "user",
                    PermissionCode = "read"
                },
                new RolePermission() {
                    Role = "guest",
                    PermissionCode = "read"
                },
            };
        }
    }
}
