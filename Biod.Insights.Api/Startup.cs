using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Biod.Insights.Api.Builders;
using Biod.Insights.Api.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Biod.Insights.Api.Middleware;

namespace Biod.Insights.Api
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
            // Disable default Model State filter to customize error response
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            services
                .AddControllers(c =>
                {
                    c.Filters.Add(new HttpResponseExceptionFilter());
                    c.Filters.Add(new ModelStateValidationFilter());
                })
                .AddNewtonsoftJson();
            services.AddSingleton(Configuration);
            services.AddApiDbContext(Configuration);
            services.ConfigureServices();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Biod.Insights.Api", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
    
            app.UseSerilogRequestLogging();

            app.UseGlobalExceptionsMiddleware();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Biod.Insights.Api V1"); });
        }
    }
}