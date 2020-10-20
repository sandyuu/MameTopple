using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MameToppleApi.Helpers;
using MameToppleApi.Hubs;
using MameToppleApi.Models;
using MameToppleApi.Repository;
using MameToppleApi.Interfaces;
using MameToppleApi.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Autofac;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.OpenApi.Models;

namespace MameToppleApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //Autofac的使用
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Autofac註冊泛型Repository
            builder.RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IRepository<>));

            // Autofac註冊所有Service結尾的Interface
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            // builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
            //     .Where(x => x.GetCustomAttribute<DependencyInjection>() != null)
            //     .AsImplementedInterfaces()
            //     .InstancePerLifetimeScope();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSpaStaticFiles(options => options.RootPath = "MameVue/dist");
            services.AddDbContext<ToppleDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ToppleDBContext")));
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                    });
            });
            services.AddSignalR(option =>
            {
                option.EnableDetailedErrors = true;
                option.KeepAliveInterval = TimeSpan.FromMinutes(1);
            }); // include signalR service
            services.AddSingleton<JwtHelpers>();//註冊JwtHelpers
            //註冊Swagger，定義一個或多個Swagger文件。
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "在下框中輸入請求頭中需要新增Jwt授權Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
                var baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            //註冊JWT  
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                // 當驗證失敗時，回應標頭會包含 WWW-Authenticate 標頭，這裡會顯示失敗的詳細錯誤原因
                options.IncludeErrorDetails = true; // 預設值為 true，有時會特別關閉

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // 透過這項宣告，就可以從 "sub" 取值並設定給 User.Identity.Name
                    NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                    // 透過這項宣告，就可以從 "roles" 取值，並可讓 [Authorize] 判斷角色
                    RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",

                    // 一般我們都會驗證 Issuer
                    ValidateIssuer = true,
                    ValidIssuer = Configuration.GetValue<string>("JwtSettings:Issuer"),

                    // 通常不太需要驗證 Audience
                    ValidateAudience = false,
                    //ValidAudience = "JwtAuthDemo", // 不驗證就不需要填寫

                    // 一般我們都會驗證 Token 的有效期間
                    ValidateLifetime = true,

                    // 如果 Token 中包含 key 才需要驗證，一般都只有簽章而已
                    ValidateIssuerSigningKey = false,

                    // "1234567890123456" 應該從 IConfiguration 取得
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetValue<string>("JwtSettings:SignKey")))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication(); //驗證

            app.UseAuthorization(); //授權

            app.UseSwagger(); //啟用中介軟體為產生的 JSON 文件和 Swagger UI 提供服務

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MameHub>("/mamehub");
            });

            //啟用中介軟體開啟Vue頁面
            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "MameVue";
                if (env.IsDevelopment())
                {
                    spa.UseVueDevelopmentServer();
                }
            });

            //啟用中介軟體提供swagger - ui(HTML, JS, CSS, etc.),
            // 指定 Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
