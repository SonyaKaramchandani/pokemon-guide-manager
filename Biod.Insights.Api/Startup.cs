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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace Biod.Insights.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IWebHostEnvironment Environment { get; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.IsDevelopment() || Environment.IsStaging())
            {
                // Add CORS policy
                services.AddCors(options =>
                {
                    options.AddPolicy("CorsPolicy",
                        builder => builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader() );
                });
            }
            
            // Disable default Model State filter to customize error response
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            services
                .AddControllers(c =>
                {
                    c.Filters.Add(new HttpResponseExceptionFilter());
                    c.Filters.Add(new ModelStateValidationFilter());

                    if (Environment.IsDevelopment())
                    { //this will allow anonymous to all api endpints.
                      //TODO: remove this when the UI is ready to handle jwt because, currently the identity User is not available
                        c.Filters.Add<AllowAnonymousFilter>();
                    }
                    else
                    {
                        var authenticatedUserPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                                  .RequireAuthenticatedUser()
                                  .Build();
                        c.Filters.Add(new AuthorizeFilter(authenticatedUserPolicy));
                    }


                })
                .AddNewtonsoftJson();
            services.AddAuthentication(Configuration);
            services.AddMvc();
            services.AddSingleton(Configuration);
            services.AddApiDbContext(Configuration);
            services.AddHttpClients(Configuration);
            services.ConfigureServices();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Biod.Insights.Api", Version = "v1"}); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || Environment.IsStaging())
            {
                app.UseDeveloperExceptionPage();
                
                // Use the CORS policy
                app.UseCors("CorsPolicy");
            }
            else
            {
                app.UseGlobalExceptionsMiddleware();
            }
    
            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "Biod.Insights.Api V1"); });
        }
    }
}