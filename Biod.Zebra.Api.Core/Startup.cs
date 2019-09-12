using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Biod.Zebra.Api.Core.Models;
using Biod.Zebra.Api.Core.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace Biod.Zebra.Api.Core
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            //object graph configuration
            var appConfig = AppSettingsReader.GetAppSettings();

            //load the xml document
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            var xmlPath = Path.Combine(basePath, "Biod.Zebra.Api.Core.xml");

            //Added the authentication using AddAuthentication which will add the authentication services to DI and configures Bearer as the default scheme
            //AddIdentityServerAuthentication adds the IdentityServer access token validation handler into DI for use by the authentication services
            //services.AddMvc();
            services.AddMvcCore()
                .AddApiExplorer()
                .AddAuthorization()
                .AddJsonFormatters();

            //To generate the Models from the database https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/existing-db
            //services.AddDbContext<BiodApiContext>(options => options.UseSqlServer(appConfig.ConnectionString));

            //register swagger gen
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "BlueDot Inc. API Version 1",
                    Description = "BlueDot Inc. ASP.NET Core 2.0 Swagger Web API",
                    TermsOfService = "This is the terms of services",
                    Contact = new Contact()
                    {
                        Name = appConfig.CompanyName,
                        Email = appConfig.SupportEmail,
                        Url = appConfig.CompanyUrl
                    }
                });
                c.IncludeXmlComments(xmlPath);
            });

            services.AddAuthentication("Bearer")
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = appConfig.AuthenticationAuthority;
                options.RequireHttpsMetadata = false;

                options.ApiName = appConfig.AuthenticationApiName;
            });
        }

        /// <summary>
        /// This method gets called by the runtime. This method is to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            if (env.IsProduction() || env.IsStaging())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            app.UseAuthentication();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // /Biod.Zebra.Api.Core/swagger/v1/swagger.json
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlueDot Inc. API V1");
            });
        }
    }
}
