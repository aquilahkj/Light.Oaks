using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Light.Oaks
{
    class ExceptionFilter : IExceptionFilter
    {
        readonly IExceptionManagement exceptionManagement;

        public ExceptionFilter(IExceptionManagement exceptionManagement)
        {
            this.exceptionManagement = exceptionManagement;
        }

        public void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;
            if (exception is AggregateException) {
                exception = exception.InnerException;
            }
            var result = exceptionManagement.ProcessException(context.HttpContext, exception);
            context.HttpContext.Response.StatusCode = result.HttpStatus;
            context.Result = new JsonResult(result.HttpData);
        }
    }
}