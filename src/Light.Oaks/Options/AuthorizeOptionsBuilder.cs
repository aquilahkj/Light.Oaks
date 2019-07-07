using System;
using Microsoft.Extensions.DependencyInjection;

namespace Light.Oaks
{
    /// <summary>
    /// Authorize options builder.
    /// </summary>
    public class AuthorizeOptionsBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        public int Expiry { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool TestMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool UsePermission { get; set; }

        Action<IServiceCollection> cacheAction;

        Action<IServiceCollection> encryptorAction;

        Action<IServiceCollection> authorizeAction;

        Action<IServiceCollection> permissionAction;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceLifetime"></param>
        public void SetCache<T>(ServiceLifetime serviceLifetime = ServiceLifetime.Singleton) where T : class, ICacheAgent
        {
            void action(IServiceCollection service)
            {
                if (serviceLifetime == ServiceLifetime.Scoped) {
                    service.AddScoped<ICacheAgent, T>();
                }
                else if (serviceLifetime == ServiceLifetime.Transient) {
                    service.AddTransient<ICacheAgent, T>();
                }
                else {
                    service.AddSingleton<ICacheAgent, T>();
                }
            }
            cacheAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void SetCache<T>(T instance) where T : class, ICacheAgent
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<ICacheAgent>(instance);
            }
            cacheAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="serviceLifetime"></param>
        public void SetCache(Func<IServiceProvider, ICacheAgent> func, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
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
            cacheAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceLifetime"></param>
        public void SetEncryptor<T>(ServiceLifetime serviceLifetime = ServiceLifetime.Singleton) where T : class, IEncryptor
        {
            void action(IServiceCollection service)
            {
                if (serviceLifetime == ServiceLifetime.Scoped) {
                    service.AddScoped<IEncryptor, T>();
                }
                else if (serviceLifetime == ServiceLifetime.Transient) {
                    service.AddTransient<IEncryptor, T>();
                }
                else {
                    service.AddSingleton<IEncryptor, T>();
                }
            }
            encryptorAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void SetEncryptor<T>(T instance) where T : class, IEncryptor
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<IEncryptor>(instance);
            }
            encryptorAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="serviceLifetime"></param>
        public void SetEncryptor(Func<IServiceProvider, IEncryptor> func, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
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
            encryptorAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceLifetime"></param>
        public void SetAuthorizeModule<T>(ServiceLifetime serviceLifetime = ServiceLifetime.Singleton) where T : class, IAuthorizeModule
        {
            void action(IServiceCollection service)
            {
                if (serviceLifetime == ServiceLifetime.Scoped) {
                    service.AddScoped<IAuthorizeModule, T>();
                }
                else if (serviceLifetime == ServiceLifetime.Transient) {
                    service.AddTransient<IAuthorizeModule, T>();
                }
                else {
                    service.AddSingleton<IAuthorizeModule, T>();
                }
            }
            authorizeAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void SetAuthorizeModule<T>(T instance) where T : class, IAuthorizeModule
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<IAuthorizeModule>(instance);
            }
            authorizeAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="serviceLifetime"></param>
        public void SetAuthorizeModule(Func<IServiceProvider, IAuthorizeModule> func, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
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
            authorizeAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serviceLifetime"></param>
        public void SetPermissionModule<T>(ServiceLifetime serviceLifetime = ServiceLifetime.Singleton) where T : class, IPermissionModule
        {
            void action(IServiceCollection service)
            {
                if (serviceLifetime == ServiceLifetime.Scoped) {
                    service.AddScoped<IPermissionModule, T>();
                }
                else if (serviceLifetime == ServiceLifetime.Transient) {
                    service.AddTransient<IPermissionModule, T>();
                }
                else {
                    service.AddSingleton<IPermissionModule, T>();
                }
            }
            permissionAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public void SetPermissionModule<T>(T instance) where T : class, IPermissionModule
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<IPermissionModule>(instance);
            }
            permissionAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="serviceLifetime"></param>
        public void SetPermissionModule(Func<IServiceProvider, IPermissionModule> func, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
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
            permissionAction = action;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        internal void BuildServices(IServiceCollection services)
        {
            if (cacheAction != null) {
                cacheAction.Invoke(services);
            }
            else {
                services.AddSingleton<ICacheAgent, MemoryCacheAgent>();
            }
            if (encryptorAction != null) {
                encryptorAction.Invoke(services);
            }
            else {
                services.AddSingleton<IEncryptor, BasicEncryptor>();
            }
            if (authorizeAction != null) {
                authorizeAction.Invoke(services);
            }
            else {
                services.AddSingleton<IAuthorizeModule, BasicAuthorizeModule>();
            }
            if (UsePermission) {
                if (permissionAction != null) {
                    permissionAction.Invoke(services);
                }
                else {
                    services.AddSingleton<IPermissionModule, BasicPermissionModule>();
                }
            }
            var options = new AuthorizeOptions() {
                Expiry = Expiry,
                TestMode = TestMode
            };
            services.AddSingleton(options);
        }
    }
}
