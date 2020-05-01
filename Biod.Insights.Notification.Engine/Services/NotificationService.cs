using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Products.Common.Constants;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Notification.Engine.Models;
using Biod.Insights.Notification.Engine.Services.EmailDelivery;
using Biod.Insights.Notification.Engine.Services.EmailRendering;
using Biod.Insights.Service.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Biod.Insights.Notification.Engine.Services
{
    public class NotificationService<T> : INotificationService
    {
        protected readonly ILogger<T> _logger;
        protected readonly BiodZebraContext _biodZebraContext;
        protected readonly NotificationSettings _notificationSettings;
        protected readonly IUserService _userService;

        private readonly IEmailRenderingApiService _emailRenderingApiService;
        private readonly IEmailClientService _emailClientService;

        protected NotificationService(
            ILogger<T> logger,
            BiodZebraContext biodZebraContext,
            IOptionsMonitor<NotificationSettings> notificationSettingsAccessor,
            IEmailRenderingApiService emailRenderingApiService,
            IEmailClientService emailClientService,
            IUserService userService)
        {
            _logger = logger;
            _biodZebraContext = biodZebraContext;
            _notificationSettings = notificationSettingsAccessor.CurrentValue;
            _emailRenderingApiService = emailRenderingApiService;
            _emailClientService = emailClientService;
            _userService = userService;
        }

        public async Task SendInternalEmail(string title, string body)
        {
            await _emailClientService.SendEmailAsync(new EmailMessage
            {
                To = _notificationSettings.InternalRecipientList.Split(',').Where(e => !string.IsNullOrWhiteSpace(e)),
                Subject = title,
                Body = body
            });
        }

        public async Task<ProcessEmailResult> SendEmail(EmailViewModel emailViewModel)
        {
            var processEmailResult = new ProcessEmailResult();

            // Update the flag whether email is under testing
            emailViewModel.IsEmailTestingEnabled = _notificationSettings.EnableTestingMode;

            var body = await _emailRenderingApiService.RenderEmail(new EmailRenderingModel
            {
                Type = (int) emailViewModel.NotificationType,
                Data = emailViewModel
            });

            if (string.IsNullOrWhiteSpace(body))
            {
                _logger.LogWarning($"No email body available to be sent, skipping over email for {emailViewModel.Email}");
                return processEmailResult;
            }

            processEmailResult.RenderSuccess = true;

            processEmailResult.DeliverySuccess = await _emailClientService.SendEmailAsync(new EmailMessage
            {
                To = _notificationSettings.EnableTestingMode ? _notificationSettings.TestingRecipientList.Split(',') : new[] {emailViewModel.Email},
                Subject = emailViewModel.Title,
                Body = body
            });

            processEmailResult.SavedEmailId = await SaveSentEmail(emailViewModel, body);

            return processEmailResult;
        }

        public async Task SendEmails(IAsyncEnumerable<EmailViewModel> emailViewModels)
        {
            var results = new List<ProcessEmailResult>();
            await foreach (var emailViewModel in emailViewModels)
            {
                var result = await SendEmail(emailViewModel);
                results.Add(result);
            }

            _logger.LogInformation($"Rendered {results.Count(r => r.RenderSuccess)}, " +
                                   $"sent {results.Count(r => r.DeliverySuccess)}, " +
                                   $"and saved {results.Count(r => r.DatabaseSaveSuccess)} emails out of {results.Count}");
        }

        protected IQueryable<AspNetUsers> GetQualifyingRecipients()
        {
            return _biodZebraContext.AspNetUsers.Where(u =>
                u.EmailConfirmed
                && u.AspNetUserRoles.All(ur => ur.Role.Name != nameof(RoleName.UnsubscribedUsers)));
        }

        private async Task<int?> SaveSentEmail(EmailViewModel emailViewModel, string body)
        {
            try
            {
                var result = _biodZebraContext.UserEmailNotification.Add(new UserEmailNotification
                {
                    UserId = emailViewModel.UserId,
                    UserEmail = emailViewModel.Email,
                    EmailType = (int) emailViewModel.NotificationType,
                    EventId = emailViewModel.EventId,
                    Title = emailViewModel.Title,
                    Content = body + $"<!-- RAW MODEL: {JsonConvert.SerializeObject(emailViewModel)} -->",
                    SentDate = emailViewModel.SentDate,
                    AoiGeonameIds = SortGeonameIds(emailViewModel.AoiGeonameIds)
                });

                await _biodZebraContext.SaveChangesAsync();

                return result.Entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save sent email into database");
                return null;
            }
        }

        private string SortGeonameIds(string userGeonameIds)
        {
            var aoiEventInfo = string.IsNullOrEmpty(userGeonameIds) ? "" : userGeonameIds;
            var userAois = aoiEventInfo.Split(',');
            Array.Sort(userAois);
            return string.Join(",", userAois);
        }
    }
}