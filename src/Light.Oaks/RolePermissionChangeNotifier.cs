using System;
namespace Light.Oaks
{
    public sealed class RolePermissionChangeNotifier
    {
        readonly IAuthorizeManagement authorizeManagement;

        internal RolePermissionChangeNotifier(IAuthorizeManagement authorizeManagement)
        {
            this.authorizeManagement = authorizeManagement;
        }

        public void ResetRolePermission()
        {
            authorizeManagement.ResetRolePermission();
        }
    }
}
