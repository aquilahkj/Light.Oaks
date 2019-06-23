using System;
using Microsoft.AspNetCore.Http;

namespace Light.Oaks
{
    public interface IExceptionLogModule
    {
        void LogError(HttpContext httpContext, Exception exception, ExceptionModel model, ErrorResult errorResult);
    }
}
