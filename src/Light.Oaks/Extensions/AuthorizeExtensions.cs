using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Light.Oaks
{
    /// <summary>
    /// Authorize extensions.
    /// </summary>
    public static class AuthorizeExtensions
    {
        const string AUTH_INFO = "AUTH_INFO";

        const string TOKEN_INFO = "TOKEN_INFO";
        /// <summary>
        /// Sets the user info.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="account">Account.</param>
        public static void SetAuthorizeInfo(this HttpContext context, AuthorizeInfo account)
        {
            var claims = new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, account.Id),
                new Claim(ClaimTypes.Name, account.Account)
            };
            context.User.AddIdentity(new ClaimsIdentity(claims));
            context.Items[AUTH_INFO] = account;
        }

        /// <summary>
        /// Sets the token.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        public static void SetToken(this HttpContext context, string token)
        {
            context.Items[TOKEN_INFO] = token;
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <returns>The user identifier.</returns>
        /// <param name="context">Context.</param>
        public static string GetUserId(this HttpContext context)
        {
            if (context.User == null) {
                return null;
            }
            var claim = context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier);
            return claim?.Value;
        }
        /// <summary>
        /// Gets the account.
        /// </summary>
        /// <returns>The account.</returns>
        /// <param name="context">Context.</param>
        public static string GetAccount(this HttpContext context)
        {
            if (context.User == null) {
                return null;
            }
            var claim = context.User.FindFirst(x => x.Type == ClaimTypes.Name);
            return claim?.Value;
        }

        /// <summary>
        /// Gets the authorize info
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static AuthorizeInfo GetAuthorizeInfo(this HttpContext context)
        {
            if (context.Items.TryGetValue(AUTH_INFO, out object value)) {
                return (AuthorizeInfo)value;
            }
            else {
                return null;
            }
        }

        public static string GetToken(this HttpContext context)
        {
            if (context.Items.TryGetValue(TOKEN_INFO, out object value)) {
                return (string)value;
            }
            else {
                return null;
            }
        }
    }
}
