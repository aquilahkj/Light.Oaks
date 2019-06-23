using Microsoft.AspNetCore.Http;

namespace Light.Oaks
{
    /// <summary>
    /// Authorize management.
    /// </summary>
    interface IAuthorizeManagement
    {
        AuthorizeInfo VerifyAuthorize(string token);

        void ResetRolePermission();

        string TokenName { get; }
    }
}