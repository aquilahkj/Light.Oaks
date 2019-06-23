using System;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Light.Oaks
{
    class AuthorizeFilter : IActionFilter
    {
        private readonly IAuthorizeManagement authorizeManagement;

        public AuthorizeFilter(IAuthorizeManagement authorizeManagement)
        {
            this.authorizeManagement = authorizeManagement;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor) {
                var authorizeAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(AuthorizePermissionAttribute), true);
                if (authorizeAttributes.Length > 0) {
                    var httpContext = context.HttpContext;
                    var request = context.HttpContext.Request;
                    var tokens = request.Headers[authorizeManagement.TokenName];
                    string token;
                    if (tokens.Count == 0) {
                        token = string.Empty;
                    }
                    else {
                        token = tokens[0];
                    }
                    httpContext.SetToken(token);
                    var action = context.HttpContext.Request.Path;
                    AuthorizeInfo authorize = authorizeManagement.VerifyAuthorize(token);
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
