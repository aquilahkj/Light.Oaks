using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Light.Oaks
{
    /// <summary>
    /// Exception options builder.
    /// </summary>
    public class ExceptionOptionsBuilder
    {
        readonly List<ExceptonTypeModel> typeList = new List<ExceptonTypeModel>();

        /// <summary>
        /// Registers the exception.
        /// </summary>
        /// <param name="func">Func.</param>
        /// <typeparam name="T"></typeparam>
        public void RegisterException<T>(Func<T, ExceptionModel> func) where T : Exception
        {
            var nfunc = new Func<Exception, ExceptionModel>((arg) => {
                return func.Invoke(arg as T);
            });
            var model = new ExceptonTypeModel(typeof(T), nfunc);
            typeList.Add(model);
        }

        /// <summary>
        /// Registers the exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="errCode">Error Code.</param>
        /// <param name="errMsg">Error Message.</param>
        /// <param name="httpStatus">Http Status Code.</param>
        /// <param name="logType">LogType.</param>
        public void RegisterException<T>(int errCode, string errMsg = null, int httpStatus = 0, LogType logType = LogType.LogMessage) where T : Exception
        {
            ExceptionModel func(T ex)
            {
                var result = new ExceptionModel {
                    Code = errCode,
                    Message = errMsg ?? ex.Message,
                    LogType = logType,
                    HttpStatus = httpStatus
                };
                if (ex is IMultiError multi) {
                    result.Errors = multi.Errors;
                }
                return result;
            }
            RegisterException((Func<T, ExceptionModel>)func);
        }

        /// <summary>
        /// Enables the exception logger.
        /// </summary>
        public bool EnableLogger { get; set; } = true;

        /// <summary>
        /// Use ok status code
        /// </summary>
        public bool UseOkStatus { get; set; }

        Action<IServiceCollection> processAction;

        Action<IServiceCollection> loggerAction;

        Action<IServiceCollection> resultAction;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void SetExceptionModule<T>(ServiceLifetime serviceLifetime = ServiceLifetime.Singleton) where T : class, IExceptionProcessModule
        {
            void action(IServiceCollection service)
            {
                if (serviceLifetime == ServiceLifetime.Scoped) {
                    service.AddScoped<IExceptionProcessModule, T>();
                }
                else if (serviceLifetime == ServiceLifetime.Transient) {
                    service.AddTransient<IExceptionProcessModule, T>();
                }
                else {
                    service.AddSingleton<IExceptionProcessModule, T>();
                }
            }
            processAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void SetExceptionModule<T>(T instance) where T : class, IExceptionProcessModule
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<IExceptionProcessModule>(instance);
            }
            processAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="serviceLifetime"></param>
        public void SetExceptionModule(Func<IServiceProvider, IExceptionProcessModule> func, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            void action(IServiceCollection service)
            {
                if (serviceLifetime == ServiceLifetime.Scoped) {
                    service.AddScoped(func);
                }
                else if (serviceLifetime == ServiceLifetime.Transient) {
                    service.AddTransient(func);
                }
                else {
                    service.AddSingleton(func);
                }
            }
            processAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceLifetime"></param>
        public void SetExceptionLogModule<T>(ServiceLifetime serviceLifetime = ServiceLifetime.Singleton) where T : class, IExceptionLogModule
        {
            void action(IServiceCollection service)
            {
                if (serviceLifetime == ServiceLifetime.Scoped) {
                    service.AddScoped<IExceptionLogModule, T>();
                }
                else if (serviceLifetime == ServiceLifetime.Transient) {
                    service.AddTransient<IExceptionLogModule, T>();
                }
                else {
                    service.AddSingleton<IExceptionLogModule, T>();
                }
            }
            loggerAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void SetExceptionLogModule<T>(T instance) where T : class, IExceptionLogModule
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<IExceptionLogModule>(instance);
            }
            loggerAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="serviceLifetime"></param>
        public void SetExceptionLogModule(Func<IServiceProvider, IExceptionLogModule> func, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            void action(IServiceCollection service)
            {
                if (serviceLifetime == ServiceLifetime.Scoped) {
                    service.AddScoped(func);
                }
                else if (serviceLifetime == ServiceLifetime.Transient) {
                    service.AddTransient(func);
                }
                else {
                    service.AddSingleton(func);
                }
            }
            loggerAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceLifetime"></param>
        public void SetExceptionResultModule<T>(ServiceLifetime serviceLifetime = ServiceLifetime.Singleton) where T : class, IExceptionResultModule
        {
            void action(IServiceCollection service)
            {
                if (serviceLifetime == ServiceLifetime.Scoped) {
                    service.AddScoped<IExceptionResultModule, T>();
                }
                else if (serviceLifetime == ServiceLifetime.Transient) {
                    service.AddTransient<IExceptionResultModule, T>();
                }
                else {
                    service.AddSingleton<IExceptionResultModule, T>();
                }
            }
            resultAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void SetExceptionResultModule<T>(T instance) where T : class, IExceptionResultModule
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<IExceptionResultModule>(instance);
            }
            resultAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="serviceLifetime"></param>
        public void SetExceptionResultModule(Func<IServiceProvider, IExceptionResultModule> func, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            void action(IServiceCollection service)
            {
                if (serviceLifetime == ServiceLifetime.Scoped) {
                    service.AddScoped(func);
                }
                else if (serviceLifetime == ServiceLifetime.Transient) {
                    service.AddTransient(func);
                }
                else {
                    service.AddSingleton(func);
                }
            }
            resultAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        internal void BuildServices(IServiceCollection services)
        {
            if (processAction != null) {
                processAction.Invoke(services);
            }
            else {
                services.AddSingleton<IExceptionProcessModule, BasicExceptionProcessModule>();
            }
            if (EnableLogger) {
                if (loggerAction != null) {
                    loggerAction.Invoke(services);
                }
                else {
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