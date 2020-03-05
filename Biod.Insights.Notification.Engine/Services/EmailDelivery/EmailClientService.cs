using System;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Biod.Insights.Notification.Engine.Services.EmailDelivery
{
    public class EmailClientService : IEmailClientService
    {
        private readonly ILogger<EmailClientService> _logger;
        private readonly BiodZebraContext _biodZebraContext;
        private readonly NotificationSettings _notificationSettings;

        public EmailClientService(
            ILogger<EmailClientService> logger,
            BiodZebraContext biodZebraContext,
            IOptionsMonitor<NotificationSettings> notificationSettingsAccessor)
        {
            _logger = logger;
            _biodZebraContext = biodZebraContext;
            _notificationSettings = notificationSettingsAccessor.CurrentValue;
        }

        public async Task<bool> SendEmailAsync(EmailMessage message)
        {
            try
            {
                var apiKey = _notificationSettings.SendGridApiKey;
                var fromEmail = _notificationSettings.EmailSenderAddress;
                var fromName = _notificationSettings.EmailSenderName;

                var client = new SendGridClient(apiKey);
                var msg = new SendGridMessage
                {
                    From = new EmailAddress(fromEmail, fromName),
                    Subject = message.Subject,
                    HtmlContent = message.Body
                };
                msg.AddTos(message.To.Select(to => new EmailAddress(to)).ToList());
                var response = await client.SendEmailAsync(msg);
                return (int) response.StatusCode >= 200 && (int) response.StatusCode <= 299;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to send email to External Email Service for '{string.Join(',', message.To)}'", ex);
                return false;
            }
        }
    }
}