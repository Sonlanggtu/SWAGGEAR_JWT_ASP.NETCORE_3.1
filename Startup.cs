using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DemoMiddleware.Extentsions;
using DemoMiddleware.IServices;
using DemoMiddleware.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace DemoMiddleware
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

            // Add Iservice
            services.AddTransient<ILoginService, LoginService>();

            // Add JWToken
            //Add authen fixbug cannot get Claims
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;

                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["Tokens:Issuer"],
                    ValidAudience = Configuration["Tokens:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                };
            });

            // Add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Demo",
                    // Version = "1.0",
                    //Description = "This API Customer",
                    //Contact = new OpenApiContact
                    //{
                    //    Name = "DamNgocSon",
                    //    Email = "damngocsonIT@gmail.com",
                    //    Url = new Uri("https://sonlanggtu.github.io/"),
                    //}
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                var result = new OpenApiSecurityRequirement();


                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            // Add Controller
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(">>>>> UseExceptionHandler");
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    if (ex == null) return;

                    var error = new
                    {
                        message = ex.Message
                    };

                    context.Response.ContentType = "application/json";
                    context.Response.Headers.Add("Access-Control-Allow-Credentials", new[] { "true" });
                    context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { Configuration["AllowedHosts"] });

                    using (var writer = new StreamWriter(context.Response.Body))
                    {
                        new JsonSerializer().Serialize(writer, error);
                        await writer.FlushAsync().ConfigureAwait(false);
                    }
                });
            });

            // Use Swagger
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((document, request) =>
                {
                    var paths = document.Paths.ToDictionary(item => item.Key.ToLowerInvariant(), item => item.Value);
                    document.Paths.Clear();
                    foreach (var pathItem in paths)
                    {
                        document.Paths.Add(pathItem.Key, pathItem.Value);
                    }
                });
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI Core & Dapper V1");
            });

            // check Environment
            if (env.IsDevelopment())
            {
               // app.UseDeveloperExceptionPage();
            }
            else
            {
             //   app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
              //  app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Customize MiddleWare
            //app.Use(async (res, next) =>
            //{
            //    await res.Response.WriteAsync("Middleware 1 => ");
            //    await next.Invoke();
            //    await res.Response.WriteAsync("Edit Middleware 1 => ");
            //});

            //app.Use(async (res, next) =>
            //{
            //    await res.Response.WriteAsync("Middleware 2 =>");
            //    await next.Invoke();
            //    await res.Response.WriteAsync("Edit Middleware 2 =>");
            //});

            //// app.UseMiddleware<InjectMiddleware>();

            //// use extentionMethod
            //app.UseMiddlewareExtention();

            //app.Run(async (res) =>
            //{
            //    await res.Response.WriteAsync("Middleware 4 =>");

            //});

            app.UseMiddleware<JwtMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
