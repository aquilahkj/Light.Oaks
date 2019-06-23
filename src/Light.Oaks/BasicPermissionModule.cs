using System;
namespace Light.Oaks
{
    public class BasicPermissionModule : IPermissionModule
    {
        public BasicPermissionModule()
        {

        }

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
