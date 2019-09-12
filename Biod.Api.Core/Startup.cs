using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Biod.Api.Core.Models;
using Microsoft.EntityFrameworkCore;
using Biod.Api.Core.Settings;

namespace Biod.Api.Core
{
    /// <summary>
    /// The startup class
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

        /// <summary>
        /// This method gets called by the runtime. This method is to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<IISOptions>(options =>
            //{
            //    options.ForwardClientCertificate = false;
            //});

            //object graph configuration
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");

            var config = builder.Build();

            var appConfig = new AppSettings();
            config.GetSection("AppSettings").Bind(appConfig);

            //load the xml document
            var basePath = PlatformServices.Default.Application.ApplicationBasePath;
            var xmlPath = Path.Combine(basePath, "Biod.Api.Core.xml");

            services.AddMvc();

            //To generate the Models from the database https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/existing-db
            services.AddDbContext<BiodZebraContext>(options => options.UseSqlServer(appConfig.ConnectionString));

            //register swagger gen
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "BlueDot Inc. API V1.0",
                    Description = "BlueDot Inc. ASP.NET Core 2.0 Swagger Web API",
                    TermsOfService = "This is the terms of services",
                    Contact = new Contact() {
                        Name = appConfig.CompanyName,
                        Url = appConfig.CompanyUrl,
                        Email = appConfig.SupportEmail
                    }
                });

                //c.SwaggerDoc("v2", new Info
                //{
                //    Version = "v2",
                //    Title = "Biodiaspora API version 2",
                //    Description = "Biodiaspora ASP.NET Core 2.0 Swagger Web API",
                //    TermsOfService = "This is the terms of services",
                //    Contact = new Contact()
                //    {
                //        Name = appConfig.CompanyName,
                //        Email = appConfig.SupportEmail,
                //        Url = appConfig.CompanyName
                //    }
                //});

                c.IncludeXmlComments(xmlPath);
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

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // /Biod.Api/swagger/v1/swagger.json
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlueDot Inc. API V1");
                //c.SwaggerEndpoint("/Biod.Api/swagger/v2/swagger.json", "BlueDot Inc. API V2");
            });
        }
    }
}
