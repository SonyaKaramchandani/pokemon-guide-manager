using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.Models.Notification;
using Biod.Zebra.Library.Models.Notification.Email;
using Biod.Zebra.Library.Models.Notification.Push;
using Microsoft.AspNet.Identity;
using Task = System.Threading.Tasks.Task;

namespace Biod.Zebra.Library.Infrastructures.Notification
{
    public class NotificationHelper
    {
        private readonly EmailHelper _emailHelper;
        private readonly PushHelper _pushHelper;

        public NotificationHelper(BiodZebraEntities dbContext, UserManager<ApplicationUser> userManager = null)
        {
            _emailHelper = new EmailHelper(dbContext, userManager);
            _pushHelper = new PushHelper();
        }

        public async Task SendZebraNotifications(IEnumerable<NotificationViewModel> emailViewModels)
        {
            await Task.WhenAll(emailViewModels.Select(SendZebraNotification).ToArray());
        }

        public async Task SendZebraNotification(INotificationViewModel viewModel)
        {
            var dbEmailId = -1;
            if (viewModel is IEmailViewModel emailViewModel)
            {
                dbEmailId = await _emailHelper.SendZebraEmail(emailViewModel);
            }

            if (dbEmailId > 0 && viewModel is IPushViewModel pushViewModel)
            {
                pushViewModel.NotificationId = dbEmailId;  // set the ID to the matching email DB row to allow subsequent query for HTML body
                await _pushHelper.SendZebraPushNotifications(pushViewModel);
            }
        }

        /// <summary>
        /// Checks if the notification should be sent to a user. The notification will not be sent to a user if:
        /// <list type="bullet">
        /// <item>User is unsubscribed</item>
        /// <item>User has not confirmed the email</item>
        /// </list>
        /// </summary>
        /// <returns>True if the notification should be sent, false otherwise.</returns>
        public static bool ShouldSendNotification(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            return !userManager.IsInRole(user.Id, ConfigurationManager.AppSettings.Get("UnsubscribedUsersRole")) && user.EmailConfirmed;
        }
    }
}