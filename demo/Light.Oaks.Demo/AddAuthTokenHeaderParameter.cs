using System;
using System.Collections.Generic;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Light.Oaks.Demo
{
    /// <summary>
    /// Add auth token header parameter.
    /// </summary>
    public class AddAuthTokenHeaderParameter : IOperationFilter
    {
        /// <summary>
        /// Apply the specified operation and context.
        /// </summary>
        /// <param name="operation">Operation.</param>
        /// <param name="context">Context.</param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();
            if (context.ApiDescription.TryGetMethodInfo(out MethodInfo methodInfo)) {
                var attrs = methodInfo.GetCustomAttributes<AuthorizePermissionAttribute>();
                foreach (var attr in attrs) {
                    // 如果 Attribute 是我们自定义的验证过滤器
                    operation.Parameters.Add(new NonBodyParameter() {
                        Name = "X-Token",
                        Description = "鉴权令牌",
                        In = "header",
                        Type = "string",
                        Required = false
                    });
                }
            }

        }
    }
}