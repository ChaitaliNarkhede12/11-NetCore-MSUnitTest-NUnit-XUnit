using ApplicationLayer.Interfaces;
using ApplicationLayer.Services;
using DataAccess.Interfaces;
using DataAccess.Models;
//using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace NetCoreWebApi
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
            services.AddControllers();

            services.AddDbContext<ContactDBContext>(item => item.UseSqlServer
              (Configuration.GetConnectionString("ContactDBConnection")));

            services.AddCors(option => option.AddPolicy("ContactPolicy",
                builder =>
                {
                    builder.WithOrigins("http://localhost:8200")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                })
            );

            //Register DI depencies 
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IContactDetailsApp,ContactDetailsAppService>();

            //Add Swagger relates setting  
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Demo Employee API",
                    Version = "v1.1",
                    Description = "API to unerstand request and response schema.",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("ContactPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo Employee API");
            });

        }
    }
}
