using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Light.Oaks
{
    /// <summary>
    /// Exception management.
    /// </summary>
    interface IExceptionManagement
    {
        ExceptionProcessResult ProcessException(HttpContext context, Exception ex);
    }
}