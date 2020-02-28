using System;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Notification.Engine.Services.EmailDelivery;
using Biod.Insights.Notification.Engine.Services.EmailRendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Biod.Insights.Notification.Engine.Services.Proximal
{
    public class ProximalNotificationService : IProximalNotificationService
    {
        private readonly ILogger<ProximalNotificationService> _logger;
        private readonly BiodZebraContext _biodZebraContext;
        private readonly NotificationSettings _notificationSettings;
        private readonly IEmailClientService _emailClientService;
        private readonly IEmailRenderingApiService _emailRenderingApiService;

        public ProximalNotificationService(
            ILogger<ProximalNotificationService> logger,
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

        public Task ProcessRequest(int eventId)
        {
            throw new System.NotImplementedException();
        }
    }
}