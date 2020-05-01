using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Biod.Insights.Notification.Engine;
using System.Reflection;
using Biod.Products.Common.Filters;
using Biod.Insights.Data;
using Biod.Insights.Service.Builders;
using Serilog;

namespace Biod.Insights.Notification.Api
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
            // Add controllers
            services.AddControllers(c =>
            {
                c.Filters.Add(new HttpResponseExceptionFilter());
            });
            
            // Add the notification services
            services.AddNotificationEngineServices(Configuration);
            
            // Add database
            services.AddDataDbContext(Configuration);

            services.ConfigureServices(Configuration);
            
            // Add swagger documentation
            services.AddSwaggerGen(c =>
            {
                var appConfig = Configuration.GetSection("AppSettings").Get<AppSettings>();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "BlueDot Inc. API V1.0",
                    Description = "BlueDot Inc. Insights Notification API",
                    License = new OpenApiLicense()
                    {
                        Name = appConfig.CompanyName,
                        Url = new Uri(appConfig.CompanyUrl)
                    },
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Biod.Insights.Notification.Api V1");
            });
        }
    }
}
