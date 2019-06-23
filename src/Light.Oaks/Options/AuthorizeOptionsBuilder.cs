using System;
using Microsoft.Extensions.DependencyInjection;

namespace Light.Oaks
{
    /// <summary>
    /// Authorize options builder.
    /// </summary>
    public class AuthorizeOptionsBuilder
    {
        public int Expiry { get; set; }

        public bool TestMode { get; set; }

        Action<IServiceCollection> cacheAction;

        Action<IServiceCollection> encryptorAction;

        Action<IServiceCollection> authorizeAction;

        Action<IServiceCollection> permissionAction;

        public void SetCache<T>() where T : class, ICacheAgent
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<ICacheAgent, T>();
            }
            cacheAction = action;
        }

        public void SetCache<T>(T instance) where T : class, ICacheAgent
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<ICacheAgent>(instance);
            }
            cacheAction = action;
        }

        public void SetCache(Func<IServiceProvider, ICacheAgent> func)
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton(func);
            }
            cacheAction = action;
        }

        public void SetEncryptor<T>() where T : class, IEncryptor
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<IEncryptor, T>();
            }
            encryptorAction = action;
        }

        public void SetEncryptor<T>(T instance) where T : class, IEncryptor
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<IEncryptor>(instance);
            }
            encryptorAction = action;
        }

        public void SetEncryptor(Func<IServiceProvider, IEncryptor> func)
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton(func);
            }
            encryptorAction = action;
        }

        public void SetAuthorizeModule<T>() where T : class, IAuthorizeModule
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<IAuthorizeModule, T>();
            }
            authorizeAction = action;
        }

        public void SetAuthorizeModule<T>(T instance) where T : class, IAuthorizeModule
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<IAuthorizeModule>(instance);
            }
            authorizeAction = action;
        }

        public void SetAuthorizeModule(Func<IServiceProvider, IAuthorizeModule> func)
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton(func);
            }
            authorizeAction = action;
        }

        public void SetPermissionModule<T>() where T : class, IPermissionModule
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<IPermissionModule, T>();
            }
            permissionAction = action;
        }

        public void SetPermissionModule<T>(T instance) where T : class, IPermissionModule
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton<IPermissionModule>(instance);
            }
            permissionAction = action;
        }

        public void SetPermissionModule(Func<IServiceProvider, IPermissionModule> func)
        {
            void action(IServiceCollection service)
            {
                service.AddSingleton(func);
            }
            permissionAction = action;
        }

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
            if (permissionAction != null) {
                permissionAction.Invoke(services);
            }
            else {
                services.AddSingleton<IPermissionModule, BasicPermissionModule>();
            }
            var options = new AuthorizeOptions() {
                Expiry = Expiry,
                TestMode = TestMode
            };
            services.AddSingleton(options);
        }
    }
}
