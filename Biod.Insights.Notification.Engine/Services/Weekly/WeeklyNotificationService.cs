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
    public class WeeklyNotificationService : IWeeklyNotificationService
    {
        private readonly ILogger<WeeklyNotificationService> _logger;
        private readonly BiodZebraContext _biodZebraContext;
        private readonly NotificationSettings _notificationSettings;
        private readonly IEmailClientService _emailClientService;
        private readonly IEmailRenderingApiService _emailRenderingApiService;

        public WeeklyNotificationService(
            ILogger<WeeklyNotificationService> logger,
            BiodZebraContext biodZebraContext,
            IOptionsMonitor<NotificationSettings> notificationSettingsAccessor,
            IEmailClientService emailClientService,
            IEmailRenderingApiService emailRenderingApiService)
        {
            _logger = logger;
            _biodZebraContext = biodZebraContext;
            _notificationSettings = notificationSettingsAccessor.CurrentValue;
            _emailClientService = emailClientService;
            _emailRenderingApiService = emailRenderingApiService;
        }

        public async Task ProcessRequest()
        {
            try
            {
                var text = await _emailRenderingApiService.RenderEmail(new EmailRenderingModel
                {
                    Type = (int) NotificationType.WeeklyBrief,
                    Data = new WeeklyViewModel
                    {
                        Email = "kevin@bluedot.global",
                        UserId = "1f6bb27a-8a8c-45c7-8952-033731fcef3b",
                        Title = "Test Email",
                        IsEmailConfirmed = true,
                        EventId = null,
                        SentDate = DateTimeOffset.Now,
                        AoiGeonameIds = "6167865",
                        IsDoNotTrackEnabled = false
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Failed to render email for ...", ex);
            }
            throw new NotImplementedException();
        }
    }
}