using System;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Light.Oaks
{
    class AuthorizeFilter : IActionFilter
    {
        readonly IAuthorizeModule authorizeModule;

        readonly PermissionManagement permissionManagement;

        readonly IPermissionModule permissionModule;

        public AuthorizeFilter(IServiceProvider serviceProvider)
        {
            authorizeModule = serviceProvider.GetRequiredService<IAuthorizeModule>();
            permissionManagement = serviceProvider.GetRequiredService<PermissionManagement>();
            permissionModule = serviceProvider.GetService<IPermissionModule>();
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        private AuthorizeInfo VerifyAuthorize(string token)
        {
            if (permissionModule != null) {
                if (!permissionManagement.HasLoad) {
                    lock (permissionManagement) {
                        if (!permissionManagement.HasLoad) {
                            permissionManagement.SetRoles(permissionModule.GetRoles());
                        }
                    }
                }
            }

            var verifyInfo = authorizeModule.VerifyToken(token);
            var authorizeInfo = new AuthorizeInfo() {
                Id = verifyInfo.Id,
                Account = verifyInfo.Account,
                CreateTime = verifyInfo.CreateTime,
                Key = verifyInfo.Key,
                Name = verifyInfo.Name,
                Roles = permissionManagement.GetRoleCollection(verifyInfo.Roles)
            };
            return authorizeInfo;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor) {
                var authorizeAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(AuthorizePermissionAttribute), true);
                if (authorizeAttributes.Length > 0) {
                    var httpContext = context.HttpContext;
                    var request = context.HttpContext.Request;
                    var tokenName = authorizeModule.ToeknName;
                    if (string.IsNullOrEmpty(tokenName)) {
                        tokenName = "X-Token";
                    }
                    var tokens = request.Headers[tokenName];
                    string token;
                    if (tokens.Count == 0) {
                        token = string.Empty;
                    }
                    else {
                        token = tokens[0];
                    }
                    httpContext.SetToken(token);
                    var action = context.HttpContext.Request.Path;
                    AuthorizeInfo authorize = VerifyAuthorize(token);
                    httpContext.SetAuthorizeInfo(authorize);
                    var authorizeAttribute = (AuthorizePermissionAttribute)authorizeAttributes[0];

                    if (authorizeAttribute.Permission != null) {
                        foreach (var permission in authorizeAttribute.Permission) {
                            if (authorize.Roles.CheckPermission(permission)) {
                                return;
                            }
                        }
                        throw new PermissionException(SR.AccountNotPermission, authorize.Account, action);
                    }
                }

            }
        }
    }
}
