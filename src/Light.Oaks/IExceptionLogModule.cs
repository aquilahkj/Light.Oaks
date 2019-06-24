using System;
using Microsoft.AspNetCore.Http;

namespace Light.Oaks
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExceptionLogModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="exception"></param>
        /// <param name="model"></param>
        /// <param name="errorResult"></param>
        void LogError(HttpContext httpContext, Exception exception, ExceptionModel model, ErrorResult errorResult);
    }
}
