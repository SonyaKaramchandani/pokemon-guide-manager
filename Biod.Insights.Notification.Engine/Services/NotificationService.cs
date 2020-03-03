using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Notification.Engine.Models;
using Biod.Insights.Notification.Engine.Services.EmailDelivery;
using Biod.Insights.Notification.Engine.Services.EmailRendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Biod.Insights.Notification.Engine.Services
{
    public class NotificationService<T> : INotificationService
    {
        protected readonly ILogger<T> _logger;
        protected readonly BiodZebraContext _biodZebraContext;
        protected readonly NotificationSettings _notificationSettings;
        protected readonly IEmailRenderingApiService _emailRenderingApiService;
        protected readonly IEmailClientService _emailClientService;

        protected NotificationService(
            ILogger<T> logger,
            BiodZebraContext biodZebraContext,
            IOptionsMonitor<NotificationSettings> notificationSettingsAccessor,
            IEmailRenderingApiService emailRenderingApiService,
            IEmailClientService emailClientService)
        {
            _logger = logger;
            _biodZebraContext = biodZebraContext;
            _notificationSettings = notificationSettingsAccessor.CurrentValue;
            _emailRenderingApiService = emailRenderingApiService;
            _emailClientService = emailClientService;
        }

        public async Task SendEmail(NotificationSettings notificationSettings, EmailViewModel emailViewModel)
        {
            emailViewModel.Email = "kevin@bluedot.global";
            var body = await _emailRenderingApiService.RenderEmail(new EmailRenderingModel
            {
                Type = (int) emailViewModel.NotificationType,
                Data = emailViewModel
            });
            
            // TODO: Check whether email under testing, if yes, override email to sent to testing email not real user email

            await _emailClientService.SendEmailAsync(new EmailMessage
            {
                To = new List<string> {emailViewModel.Email},
                Subject = emailViewModel.Title,
                Body = body
            });
        }

        public async Task SendEmails(NotificationSettings notificationSettings, IAsyncEnumerable<EmailViewModel> emailViewModels)
        {
            await foreach (var emailViewModel in emailViewModels)
            {
                await SendEmail(notificationSettings, emailViewModel);
            }
        }
    }
}