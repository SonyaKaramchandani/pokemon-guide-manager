using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Notification.Engine.Services.EmailDelivery;
using Biod.Insights.Notification.Engine.Services.EmailRendering;
using Biod.Insights.Service.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Biod.Insights.Notification.Engine.Services.Internal
{
    public class InternalNotificationService : NotificationService<InternalNotificationService>, IInternalNotificationService
    {
        public InternalNotificationService(
            ILogger<InternalNotificationService> logger,
            BiodZebraContext biodZebraContext,
            IOptionsMonitor<NotificationSettings> notificationSettingsAccessor,
            IEmailRenderingApiService emailRenderingApiService,
            IEmailClientService emailClientService,
            IUserService userService) : base(logger, biodZebraContext, notificationSettingsAccessor, emailRenderingApiService, emailClientService, userService)
        {
        }
        
        public async Task ProcessRequest(string subject, string body)
        {
            _logger.LogInformation("Sending Internal Email: {0}\n{1}", subject, body);
            await SendInternalEmail(subject, body);
        }
    }
}