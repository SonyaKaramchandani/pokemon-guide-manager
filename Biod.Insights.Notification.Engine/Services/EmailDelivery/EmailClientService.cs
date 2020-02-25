using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Notification.Engine.Services.Proximal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Biod.Insights.Notification.Engine.Services.EmailDelivery
{
    public class EmailClientService : IEmailClientService
    {
        private readonly ILogger<ProximalNotificationService> _logger;
        private readonly BiodZebraContext _biodZebraContext;
        private readonly NotificationSettings _notificationSettings;
        
        public EmailClientService(
            ILogger<ProximalNotificationService> logger,
            BiodZebraContext biodZebraContext,
            IOptionsMonitor<NotificationSettings> notificationSettingsAccessor)
        {
            _logger = logger;
            _biodZebraContext = biodZebraContext;
            _notificationSettings = notificationSettingsAccessor.CurrentValue;
        }
        
        public Task SendEmailAsync(EmailMessage message)
        {
            throw new System.NotImplementedException();
        }
    }
}