using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Light.Oaks;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Text;
using NLog.Extensions.Logging;

namespace Light.Oaks.Demo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
             .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
             .ConfigureApiBehaviorOptions(options => {
                 options.UseInvalidModelStateException();
             });
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Info { Title = "OAKS DEMO API", Version = "v1" });
                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                //获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "Light.Oaks.Demo.xml");
                c.IncludeXmlComments(xmlPath);
                c.OperationFilter<AddAuthTokenHeaderParameter>(); // 手动高亮
            });

            services.AddAuthorize(builder => {
                builder.UseDesEncryptor("aabb");
            });

            services.AddException(builder => {
                builder.EnableExceptionLogger();
                builder. 
                builder.RegisterType<AuthorizeException>(x => {
                    return new ExceptionModel() {
                        Code = 40101,
                        Message = "ex:" + x.Message,
                        HttpStatus = 401,
                        LogType = LogType.LogTraceId | LogType.LogPostData | LogType.LogFullException
                    };
                });
                builder.RegisterCode<PermissionException>(40301, 403);
                builder.RegisterCode<SubPermissionException>(40302, 403, LogType.LogTraceId);
                builder.RegisterCode<CustomizeException>(41000, 410, LogType.LogTraceId | LogType.LogFullException);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            loggerFactory.AddNLog();
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.ShowExtensions();
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OAKS DEMO API V1");
            });
        }
    }
}
