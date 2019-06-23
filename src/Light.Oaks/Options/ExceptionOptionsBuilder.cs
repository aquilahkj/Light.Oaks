using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Light.Oaks
{
    /// <summary>
    /// Exception options builder.
    /// </summary>
    public class  ExceptionOptionsBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Light.Oaks.ExceptionOptionsBuilder"/> class.
        /// </summary>
        public ExceptionOptionsBuilder()
        {
        }


        readonly List<ExceptonTypeModel> typeList = new List<ExceptonTypeModel>();

        bool exceptionLogger;

        /// <summary>
        /// Registers the type.
        /// </summary>
        /// <param name="func">Func.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public void RegisterType<T>(Func<T, ExceptionModel> func) where T : Exception
        {
            var nfunc = new Func<Exception, ExceptionModel>((arg) => {
                return func.Invoke(arg as T);
            });
            var model = new ExceptonTypeModel(typeof(T), nfunc);
            typeList.Add(model);
        }

        /// <summary>
        /// Registers the code.
        /// </summary>
        /// <param name="errCode">Error code.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public void RegisterCode<T>(int errCode, int httpStatus = 0, LogType logType = LogType.LogMessage) where T : Exception
        {
            Func<T, ExceptionModel> func = (ex) => {
                var result = new ExceptionModel {
                    Code = errCode,
                    Message = ex.Message,
                    LogType = logType,
                    HttpStatus = httpStatus
                };
                if (ex is IMultiError multi) {
                    result.Errors = multi.Errors;
                }
                return result;
            };
            RegisterType(func);
        }

        /// <summary>
        /// Enables the exception logger.
        /// </summary>
        public void EnableExceptionLogger()
        {
            exceptionLogger = true;
        }

        Action<IServiceCollection> processAction;

        Action<IServiceCollection> loggerAction;

        Action<IServiceCollection> resultAction;


        public void SetExceptionModule<T>() where T : class, IExceptionProcessModule
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<IExceptionProcessModule, T>();
            }
            processAction = action;
        }

        public void SetExceptionModule<T>(T instance) where T : class, IExceptionProcessModule
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<IExceptionProcessModule>(instance);
            }
            processAction = action;
        }

        public void SetExceptionModule(Func<IServiceProvider, IExceptionProcessModule> func)
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton(func);
            }
            processAction = action;
        }

        public void SetExceptionLogModule<T>() where T : class, IExceptionLogModule
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<IExceptionLogModule, T>();
            }
            loggerAction = action;
        }

        public void SetExceptionLogModule<T>(T instance) where T : class, IExceptionLogModule
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<IExceptionLogModule>(instance);
            }
            loggerAction = action;
        }

        public void SetExceptionLogModule(Func<IServiceProvider, IExceptionLogModule> func)
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton(func);
            }
            loggerAction = action;
        }

        public void SetExceptionResultModule<T>() where T : class, IExceptionResultModule
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<IExceptionResultModule, T>();
            }
            loggerAction = action;
        }

        public void SetExceptionResultModule<T>(T instance) where T : class, IExceptionResultModule
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<IExceptionResultModule>(instance);
            }
            loggerAction = action;
        }

        public void SetExceptionResultModule(Func<IServiceProvider, IExceptionResultModule> func)
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton(func);
            }
            loggerAction = action;
        }

        public bool UseOkStatus { get; set; }

        internal void BuildServices(IServiceCollection services)
        {
            if (processAction != null) {
                processAction.Invoke(services);
            }
            else {
                services.AddSingleton<IExceptionProcessModule, BasicExceptionProcessModule>();
            }
            if (loggerAction != null) {
                loggerAction.Invoke(services);
            }
            else {
                if (exceptionLogger) {
                    services.AddSingleton<IExceptionLogModule, BasicExceptionLogModule>();
                }
            }
            if (resultAction != null) {
                resultAction.Invoke(services);
            }
            else {
                services.AddSingleton<IExceptionResultModule, BasicExceptionResultModule>();
            }
            var options = new ExceptionOptions() {
                UseOkStatus = UseOkStatus,
                ExceptionTypes = typeList
            };
            services.AddSingleton(options);
        }
    }
}
