using System;
using Biod.Products.Common.Constants;
using Biod.Products.Common.HttpClients;
using Biod.Insights.Notification.Engine.Services.EmailDelivery;
using Biod.Insights.Notification.Engine.Services.EmailRendering;
using Biod.Insights.Notification.Engine.Services.NewEvent;
using Biod.Insights.Notification.Engine.Services.Proximal;
using Biod.Insights.Notification.Engine.Services.Weekly;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Biod.Insights.Notification.Engine
{
    public static class NotificationServicesBuilder
    {
        /// <summary>
        /// Configure dependency injected notification services 
        /// </summary>
        /// <param name="services">the services during startup</param>
        /// <param name="configuration">the configuration during startup</param>
        /// <returns>the services after configuration</returns>
        public static IServiceCollection AddNotificationEngineServices(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection(GlobalVariables.AppSettingsSection.NOTIFICATION_SETTINGS);
            if (settings == null)
            {
                throw new Exception($"The section '{GlobalVariables.AppSettingsSection.NOTIFICATION_SETTINGS}' settings section is missing!");
            }
            
            services.Configure<NotificationSettings>(settings);
            services.AddScoped<IEmailClientService, EmailClientService>();
            services.AddHttpClients<IEmailRenderingApiService, EmailRenderingApiService>(settings.Get<NotificationSettings>().EmailRenderingApiSettings);
            
            // Add Notification Services here
            services.AddScoped<IProximalNotificationService, ProximalNotificationService>();
            services.AddScoped<IWeeklyNotificationService, WeeklyNotificationService>();
            services.AddScoped<INewEventNotificationService, NewEventNotificationService>();
            
            return services;
        }
    }
}