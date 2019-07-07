using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Light.Oaks
{
    class ExceptionFilter : IExceptionFilter
    {
        readonly IExceptionProcessModule exceptionProcessModule;
        readonly IExceptionLogModule exceptionLogModule;
        readonly IExceptionResultModule exceptionResultModule;

        public ExceptionFilter(IServiceProvider serviceProvider)
        {
            exceptionProcessModule = serviceProvider.GetService<IExceptionProcessModule>();
            if (exceptionProcessModule == null) {
                var options = serviceProvider.GetService<ExceptionOptions>();
                if (options == null) {
                    options = new ExceptionOptions();
                }
                exceptionProcessModule = new BasicExceptionProcessModule(options);
            }
            exceptionLogModule = serviceProvider.GetService<IExceptionLogModule>();
            exceptionResultModule = serviceProvider.GetService<IExceptionResultModule>();
        }

        //readonly IExceptionManagement exceptionManagement;

        //public ExceptionFilter(IExceptionManagement exceptionManagement)
        //{
        //    this.exceptionManagement = exceptionManagement;
        //}

        public void OnException(ExceptionContext context)
        {
            Exception ex = context.Exception;
            if (ex is AggregateException) {
                ex = ex.InnerException;
            }
            var model = exceptionProcessModule.ProcessException(ex);
            var errorResult = new ErrorResult() {
                Code = model.Code,
                Message = model.Message,
                Errors = model.Errors,
            };
            if ((model.LogType & LogType.LogTraceId) == LogType.LogTraceId) {
                errorResult.TraceId = Guid.NewGuid().ToString("D");
            }
            if (exceptionLogModule != null) {
                exceptionLogModule.LogError(context.HttpContext, ex, model, errorResult);
            }
            object httpData;
            if (exceptionResultModule != null) {
                httpData = exceptionResultModule.CreateResult(ex, errorResult);
            }
            else {
                httpData = errorResult;
            }
            context.HttpContext.Response.StatusCode = model.HttpStatus;
            context.Result = new JsonResult(httpData);
        }
    }
}