using System;
using System.Net;
using System.Net.Http;
using Biod.Products.Common.Constants;
using Biod.Products.Common.HttpClients;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Biod.Insights.Service.Builders
{
    public static class DataSystemsHealthCareWorkerServicesBuilder
    {
        public static IServiceCollection AddDataSystemsHealthCareWorkerServices(this IServiceCollection services, IConfiguration configuration)
        {
            var dataSystemsHealthCareWorkerApiSettings =
                configuration.GetSection(GlobalVariables.AppSettingsSection.DATASYSTEMS_HEALTHCAREWORKER_API_SETTINGS).Get<DataSystemsHeathCareWorkerApiSettings>();
            if (dataSystemsHealthCareWorkerApiSettings == null)
            {
                throw new Exception($"The section '{GlobalVariables.AppSettingsSection.DATASYSTEMS_HEALTHCAREWORKER_API_SETTINGS}' settings section is missing!");
            }

            services.AddHttpClients<IDataSystemsHealthCareWorkerApiService, DataSystemsHealthCareWorkerApiService>(
                dataSystemsHealthCareWorkerApiSettings, httpClientBuilder =>
                {
                    // Data Systems Health Care Worker Api include network credentials
                    httpClientBuilder.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                    {
                        Credentials = new NetworkCredential(dataSystemsHealthCareWorkerApiSettings.NetworkUser, dataSystemsHealthCareWorkerApiSettings.NetworkPassword)
                    });
                });

            services.AddScoped<IHealthCareWorkerService, HealthCareWorkerService>();
            return services;
        }
    }
}