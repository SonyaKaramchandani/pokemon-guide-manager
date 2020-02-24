using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Notification.Engine.Services.EmailDelivery;
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

        public ProximalNotificationService(
            ILogger<ProximalNotificationService> logger,
            BiodZebraContext biodZebraContext,
            IOptionsMonitor<NotificationSettings> notificationSettingsAccessor,
            IEmailClientService emailClientService)
        {
            _logger = logger;
            _biodZebraContext = biodZebraContext;
            _notificationSettings = notificationSettingsAccessor.CurrentValue;
            _emailClientService = emailClientService;
        }

        public Task ProcessRequest(int eventId)
        {
            throw new System.NotImplementedException();
        }
    }
}