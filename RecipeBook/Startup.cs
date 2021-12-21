using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RecipeBook.Configuration;
using RecipeBook.Data;
using RecipeBook.IRepository;
using RecipeBook.Repository;
using RecipeBook.Services;
using System;
using System.Collections.Generic;

namespace RecipeBook
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
            services.AddDbContext<RecipeBookDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultSqlServerConnection"))
            );

            services.AddAuthentication();

            services.ConfigureIdentity();

            services.ConfigureJWT(Configuration);

            services.AddCors(setupAction =>
                    {
                        setupAction.AddPolicy("CorsPolicyAllowAll", policy =>
                       {
                           policy.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader();
                       });
                    });

            services.AddAutoMapper(typeof(MapperInitializer));

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAuthManager, AuthManager>();

            AddSwaggerDoc(services);

            services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
        }

        private void AddSwaggerDoc(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization hear using the bearer scheme .
                    Enter 'Bearer' [space] and then your token in the text input below.
                    Example : 'Bearer 123456789abcd'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"

                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme {

                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "0auth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },

                        new List<string>()

                    }
                });

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Recipe Book API",
                    Version = "v1"
                    ,
                    Description = "Recipe Book-API provides REST-APIs for Receipe Book Application for various plateforms like Angular Apps, Android Apps etc. "
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Recipe Book - v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicyAllowAll");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapControllers();
            });
        }
    }
}
