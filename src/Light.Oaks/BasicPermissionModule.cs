using System;
namespace Light.Oaks
{
    /// <summary>
    /// 
    /// </summary>
    public class BasicPermissionModule : IPermissionModule
    {
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public RolePermission[] GetRolePermissions()
        //{
        //    return new RolePermission[] {
        //        new RolePermission() {
        //            Role = "admin",
        //            Permission  = "admin"
        //        },
        //        new RolePermission() {
        //            Role = "user",
        //            Permission  = "create"
        //        },
        //        new RolePermission() {
        //            Role = "user",
        //            Permission  = "update"
        //        },
        //        new RolePermission() {
        //            Role = "user",
        //            Permission  = "delete"
        //        },
        //        new RolePermission() {
        //            Role = "user",
        //            Permission  = "read"
        //        },
        //        new RolePermission() {
        //            Role = "guest",
        //            Permission  = "read"
        //        },
        //    };
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Role[] GetRoles()
        {
            return new Role[] {
                new Role() {
                    Code = "admin",
                   Permissions= new string[] { "admin" }
                },
                new Role() {
                    Code = "user",
                    Permissions  = new string[] { "create", "update", "delete", "read" }
                },
                new Role() {
                    Code = "guest",
                    Permissions  = new string[] { "read" }
                }
            };
        }
    }
}
