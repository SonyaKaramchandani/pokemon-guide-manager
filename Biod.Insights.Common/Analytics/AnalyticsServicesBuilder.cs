using System;
using Biod.Insights.Common.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Biod.Insights.Common.Analytics
{
    public static class AnalyticsServicesBuilder
    {
        /// <summary>
        /// Configure dependency injected notification services 
        /// </summary>
        /// <param name="services">the services during startup</param>
        /// <param name="configuration">the configuration during startup</param>
        /// <returns>the services after configuration</returns>
        public static IServiceCollection AddAnalyticsServices(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection(GlobalVariables.AppSettingsSection.ANALYTICS_SETTINGS);
            if (settings == null)
            {
                throw new Exception($"The section '{GlobalVariables.AppSettingsSection.ANALYTICS_SETTINGS}' settings section is missing!");
            }
            
            services.Configure<AnalyticsSettings>(settings);
            
            return services;
        }
    }
}