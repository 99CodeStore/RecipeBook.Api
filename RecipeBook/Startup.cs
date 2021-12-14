using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RecipeBook.Data;

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
            ); ;
            services.AddCors(setupAction =>
                    {
                        setupAction.AddPolicy("CorsPolicyAllowAll", policy =>
                       {
                           policy.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader();
                       });
                    });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Recipe Book API",
                    Version = "v1"
                    ,
                    Description = "Recipe Book-API provides REST-APIs for Receipe Book Application for various plateforms like Angular Apps, Android Apps etc. "
                });
            });
            
            services.AddControllers();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
