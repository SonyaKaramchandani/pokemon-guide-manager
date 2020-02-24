using System;
using Biod.Insights.Notification.Engine.Services.EmailDelivery;
using Biod.Insights.Notification.Engine.Services.Proximal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Biod.Insights.Notification.Engine
{
    public static class NotificationServicesBuilder
    {
        private const string NOTIFICATION_SETTING_CONFIG_SECTION = "NotificationSettings";

        /// <summary>
        /// Configure dependency injected notification services 
        /// </summary>
        /// <param name="services">the services during startup</param>
        /// <param name="configuration">the configuration during startup</param>
        /// <returns>the services after configuration</returns>
        public static IServiceCollection AddNotificationEngineServices(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection(NOTIFICATION_SETTING_CONFIG_SECTION);
            if (settings == null)
            {
                throw new Exception($"The section '{NOTIFICATION_SETTING_CONFIG_SECTION}' settings section is missing!");
            }
            
            services.Configure<NotificationSettings>(settings);
            services.AddScoped<IEmailClientService, EmailClientService>();
            
            // Add Notification Services here
            services.AddScoped<IProximalNotificationService, ProximalNotificationService>();
            
            return services;
        }
    }
}