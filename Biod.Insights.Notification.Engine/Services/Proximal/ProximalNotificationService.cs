using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Notification.Engine.Services.EmailDelivery;
using Biod.Insights.Notification.Engine.Services.EmailRendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Biod.Insights.Notification.Engine.Services.Proximal
{
    public class ProximalNotificationService : NotificationService<ProximalNotificationService>, IProximalNotificationService
    {
        public ProximalNotificationService(
            ILogger<ProximalNotificationService> logger,
            BiodZebraContext biodZebraContext,
            IOptionsMonitor<NotificationSettings> notificationSettingsAccessor,
            IEmailClientService emailClientService,
            IEmailRenderingApiService emailRenderingApiService) : base(logger, biodZebraContext, notificationSettingsAccessor, emailRenderingApiService, emailClientService)
        {
        }

        public Task ProcessRequest(int eventId)
        {
            throw new System.NotImplementedException();
        }
    }
}