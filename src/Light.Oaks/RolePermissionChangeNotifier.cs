using System;
namespace Light.Oaks
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class RolePermissionChangeNotifier
    {
        readonly IAuthorizeManagement authorizeManagement;

        internal RolePermissionChangeNotifier(IAuthorizeManagement authorizeManagement)
        {
            this.authorizeManagement = authorizeManagement;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ResetRolePermission()
        {
            authorizeManagement.ResetRolePermission();
        }
    }
}
