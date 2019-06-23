using System;
namespace Light.Oaks
{
    public interface IPermissionModule
    {
        RolePermission[] GetRolePermissions();
    }
}
