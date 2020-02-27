using System;
using System.Net;
using System.Net.Http;
using Biod.Insights.Common.Constants;
using Biod.Insights.Common.HttpClients;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Biod.Insights.Service.Builders
{
    public static class ServicesBuilder
    {
        /// <summary>
        /// Configure the services required for the Api
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var georgeApiSettings = configuration.GetSection(GlobalVariables.AppSettingsSection.GEORGE_API_SETTINGS).Get<GeorgeApiSettings>();
            if (georgeApiSettings == null)
            {
                throw new Exception($"The section '{GlobalVariables.AppSettingsSection.GEORGE_API_SETTINGS}' settings section is missing!");
            }
            
            services.AddHttpClients<IGeorgeApiService, GeorgeApiService>(georgeApiSettings, httpClientBuilder =>
            {
                // George Api include network credentials
                httpClientBuilder.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    Credentials = new NetworkCredential(georgeApiSettings.NetworkUser, georgeApiSettings.NetworkPassword)
                });
            });
            
            services.AddScoped<IAirportService, AirportService>();
            services.AddScoped<ICaseCountService, CaseCountService>();
            services.AddScoped<IDiseaseService, DiseaseService>();
            services.AddScoped<IDiseaseRelevanceService, DiseaseRelevanceService>();
            services.AddScoped<IDiseaseRiskService, DiseaseRiskService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IGeonameService, GeonameService>();
            services.AddScoped<IMapService, MapService>();
            services.AddScoped<IOutbreakPotentialService, OutbreakPotentialService>();
            services.AddScoped<IRiskCalculationService, RiskCalculationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserLocationService, UserLocationService>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            return services;
        }
    }
}