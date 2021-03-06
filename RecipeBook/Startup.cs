using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using RecipeBook.Configuration;
using RecipeBook.Data;
using RecipeBook.IRepository;
using RecipeBook.Repository;
using RecipeBook.Services;


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

            services.AddMemoryCache();

            //services.ConfigureRateLimiting();

            services.AddHttpContextAccessor();

            services.ConfigureHttpCacheHeaders();


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

            services.AddControllers(config =>
            {
                config.CacheProfiles.Add("120SecondsDuration", new CacheProfile
                {
                    Duration = 120
                });
            }).AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
    
            services.AddSwaggerDoc();
            
            services.ConfigureVersioning();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                   // c.SwaggerEndpoint("/swagger/v1/swagger.json", "Recipe Book - v1");
                    string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                    c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Hotel Listing API - v1");
                }
                
                
                );
            }

            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

            app.UseCors("CorsPolicyAllowAll");

            app.UseResponseCaching();

            app.UseHttpCacheHeaders();

            //app.UseIpRateLimiting();

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
