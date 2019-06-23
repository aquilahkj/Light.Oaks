using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Light.Oaks
{
    class ExceptionManagement : IExceptionManagement
    {
        readonly IExceptionProcessModule exceptionProcessModule;

        readonly IExceptionResultModule exceptionResultModule;

        readonly IExceptionLogModule exceptionLogModule;

        public ExceptionManagement(IServiceProvider serviceProvider)
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

        public ExceptionProcessResult ProcessException(HttpContext context, Exception ex)
        {
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
                exceptionLogModule.LogError(context, ex, model, errorResult);
            }
            object httpData;
            if (exceptionResultModule != null) {
                httpData = exceptionResultModule.CreateResult(ex, errorResult);
            }
            else {
                httpData = errorResult;
            }
            return new ExceptionProcessResult() {
                HttpStatus = model.HttpStatus,
                HttpData = httpData
            };
        }
    }
}
