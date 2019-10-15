using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Infrastructures.Log;
using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.Models.Notification;
using Biod.Zebra.Library.Models.Notification.Email;
using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebMarkupMin.Core;
using Westwind.Web.Mvc;

namespace Biod.Zebra.Library.Infrastructures.Notification
{
    class EmailHelper
    {
        // Dependencies
        private readonly BiodZebraEntities _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailClient emailClient;

        private HtmlMinifier Minifier { get; set; } = new HtmlMinifier();

        public ILogger Logger { get; set; } = Log.Logger.GetLogger(typeof(NotificationHelper).ToString());

        // Configurations
        private readonly bool _messageUnderTesting = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("IsMessagingUnderTesting"));
        private readonly string[] _emailTestingRecipientList = ConfigurationManager.AppSettings.Get("EmailTestingRecipientList").Split(',');
        private readonly double _wasEmailSentTimeCheckInMinutes = Convert.ToDouble(ConfigurationManager.AppSettings.Get("WasEmailSentTimeCheckInMinutes"));

        public EmailHelper(BiodZebraEntities dbContext)
        {
            _dbContext = dbContext;
            emailClient = new EmailClient();
        }

        public EmailHelper(BiodZebraEntities dbContext, UserManager<ApplicationUser> userManager) : this(dbContext)
        {
            _userManager = userManager;
        }

        public async Task<int> SendZebraEmail(IEmailViewModel email)
        {
            email.SentDate = DateTimeOffset.UtcNow;

            var htmlBody = ViewRenderer.RenderView(email.ViewFilePath, email);
            if (htmlBody == null)
            {
                throw new ArgumentException("The provided view model does not have the path to the email view template");
            }

            htmlBody = htmlBody.Remove(0, htmlBody.IndexOf("<content>", StringComparison.Ordinal)).Replace("<content>", "");
            htmlBody = htmlBody.Remove(htmlBody.IndexOf("</content>", StringComparison.Ordinal), htmlBody.Length - htmlBody.IndexOf("</content>", StringComparison.Ordinal)).Replace("</content>", "");

            var wasEmailSentBefore = WasEmailSentBefore(
                email.Email,
                email.Title,
                htmlBody,
                email.AoiGeonameIds,
                email.UserId,
                email.NotificationType,
                email.EventId,
                email.SentDate
                );

            //Don't send email if it is existed in the database within 2 minutes (i.e. sent before)
            if (wasEmailSentBefore)
            {
                Logger.Debug($"The email was existed in the database for userId: {email.UserId}, email: {email.Email}, emailType: {email.NotificationType}, eventId: {email.EventId}, aoiEventInfo: {email.AoiGeonameIds}, subject: {email.Title}");
                return -1;
            }
            else
            {
                return await SendEmail(
                    email.Email,
                    email.Title,
                    htmlBody,
                    email.AoiGeonameIds,
                    email.UserId,
                    email.NotificationType,
                    email.EventId,
                    email.SentDate
                );
            }
        }

        private async Task<int> SendEmail(
            string email,
            string subject,
            string body,
            string aoiEventInfo,
            string userId,
            int emailType,
            int? eventId,
            DateTimeOffset emailSentDate)
        {
            // Try minification
            var result = Minifier.Minify(body);
            var minifiedBody = result.Errors.Count == 0 ? result.MinifiedContent : body;

            var message = new EmailMessage()
            {
                Subject = subject,
                Body = minifiedBody
            };
            if (_userManager != null && !_messageUnderTesting)
            {
                await _userManager.SendEmailAsync(userId, subject, minifiedBody);
            }
            else
            {
                if (!_messageUnderTesting)
                {
                    //send the email to the real user
                    message.To.Add(email);
                }
                else
                {
                    //send the alert email to the messaging under testing distribution list only
                    message.To.AddRange(_emailTestingRecipientList);
                }

                await this.emailClient.SendEmailAsync(message);
            }

            return await SaveEmailToDatabase(email, subject, body, aoiEventInfo, userId, emailType, eventId, emailSentDate);
        }

        private async Task<int> SaveEmailToDatabase(
            string email,
            string subject,
            string body,
            string aoiEventInfo,
            string userId,
            int emailType,
            int? eventId,
            DateTimeOffset emailSentDate)
        {
            try
            {
                // For analytics, store record of sent email
                var result = _dbContext.UserEmailNotifications.Add(new UserEmailNotification
                {
                    AoiGeonameIds = SortGeonameIds(aoiEventInfo),
                    UserId = userId,
                    UserEmail = email,
                    EmailType = emailType,
                    EventId = eventId,
                    Content = body,
                    SentDate = emailSentDate,
                    Title = subject
                });

                await _dbContext.SaveChangesAsync();

                return result.Id;
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to save email into database for user {userId}", ex);
                return -1;
            }
        }


        private string SortGeonameIds(string userGeonameIds)
        {
            var aoiEventInfo = string.IsNullOrEmpty(userGeonameIds) ? "" : userGeonameIds;
            var userAois = aoiEventInfo.Split(',');
            Array.Sort(userAois);
            return string.Join(",", userAois);
        }

        private bool WasEmailSentBefore(
            string email,
            string subject,
            string body,
            string aoiEventInfo,
            string userId,
            int emailType,
            int? eventId,
            DateTimeOffset emailSentDate)
        {
            try
            {
                // Check if the email record has been sent before. Return true if date is same and total min within 2 minutes
                var sortGeonameIds = SortGeonameIds(aoiEventInfo);
                return _dbContext.UserEmailNotifications
                    .Where(x => (x.AoiGeonameIds == sortGeonameIds)
                    && x.UserId == userId
                    && x.UserEmail == email
                    && x.EmailType == emailType
                    && x.EventId == eventId
                    && System.Data.Entity.DbFunctions.DiffMinutes(emailSentDate, x.SentDate) < _wasEmailSentTimeCheckInMinutes
                    && System.Data.Entity.DbFunctions.DiffMinutes(emailSentDate, x.SentDate) > 0
                    && x.Title == subject).Any();
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to check if the email existed into the database for user {userId}", ex);
                return true;
            }
        }

    }
}