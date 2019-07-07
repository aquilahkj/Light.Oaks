using System;
using Light.Oaks;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Service collection extensions.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the exception.
        /// </summary>
        /// <returns>The exception.</returns>
        /// <param name="services">Services.</param>
        /// <param name="options">Action.</param>
        public static IServiceCollection AddException(this IServiceCollection services, Action<ExceptionOptionsBuilder> options = null)
        {
            var builder = new ExceptionOptionsBuilder();
            options?.Invoke(builder);
            builder.BuildServices(services);
            //services.AddSingleton<IExceptionManagement, ExceptionManagement>();
            services.AddMvc(x => {
                x.Filters.Add<ExceptionFilter>();
            });
            return services;
        }

        /// <summary>
        /// Adds the authorize.
        /// </summary>
        /// <returns>The authorize.</returns>
        /// <param name="services">Services.</param>
        /// <param name="options">Action.</param>
        public static IServiceCollection AddAuthorize(this IServiceCollection services, Action<AuthorizeOptionsBuilder> options = null)
        {
            var builder = new AuthorizeOptionsBuilder();
            options?.Invoke(builder);
            builder.BuildServices(services);
            //services.AddSingleton<IAuthorizeManagement, AuthorizeManagement>();
            services.AddTransient<RolePermissionChangeNotifier>();
            services.AddSingleton<PermissionManagement>();
            services.AddMvc(x => {
                x.Filters.Add<AuthorizeFilter>();
            });
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="redisConfig"></param>
        /// <param name="serviceLifetime"></param>
        public static void UseRedisCache(this AuthorizeOptionsBuilder builder, string redisConfig, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            ICacheAgent func(IServiceProvider provider)
            {
                return new RedisCacheAgent(redisConfig);
            }
            builder.SetCache(func, serviceLifetime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public static void UseMemoryCache(this AuthorizeOptionsBuilder builder)
        {
            ICacheAgent func(IServiceProvider provider)
            {
                return new MemoryCacheAgent();
            }
            builder.SetCache(func);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="encryptKey"></param>
        /// <param name="serviceLifetime"></param>
        public static void UseDesEncryptor(this AuthorizeOptionsBuilder builder, string encryptKey, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            IEncryptor func(IServiceProvider provider)
            {
                return new DesEncryptor(encryptKey);
            }
            builder.SetEncryptor(func, serviceLifetime);
        }
    }
}