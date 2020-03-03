using System;
using System.Threading.Tasks;
using Biod.Insights.Common.Constants;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Notification.Engine.Models;
using Biod.Insights.Notification.Engine.Models.Weekly;
using Biod.Insights.Notification.Engine.Services.EmailDelivery;
using Biod.Insights.Notification.Engine.Services.EmailRendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Biod.Insights.Notification.Engine.Services.Weekly
{
    public class WeeklyNotificationService : NotificationService<WeeklyNotificationService>, IWeeklyNotificationService
    {
        public WeeklyNotificationService(
            ILogger<WeeklyNotificationService> logger,
            BiodZebraContext biodZebraContext,
            IOptionsMonitor<NotificationSettings> notificationSettingsAccessor,
            IEmailClientService emailClientService,
            IEmailRenderingApiService emailRenderingApiService) : base(logger, biodZebraContext, notificationSettingsAccessor, emailRenderingApiService, emailClientService)
        {
        }

        public async Task ProcessRequest()
        {
            throw new NotImplementedException();
        }
    }
}