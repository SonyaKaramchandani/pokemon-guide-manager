using Microsoft.AspNet.Identity;
using System;
using System.Configuration;


namespace Biod.Zebra.Library.Models.Notification
{
    public abstract class NotificationViewModel : INotificationViewModel
    {
        public abstract int NotificationType { get; }

        public string UserId { get; set; }

        public int? EventId { get; set; }

        public string Title { get; set; }

        public DateTimeOffset SentDate { get; set; }

        public bool DoNotTrackEnabled { get; set; }

        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Checks if the notification should be sent to a user. The notification will not be sent to a user if:
        /// <list type="bullet">
        /// <item>User is unsubscribed</item>
        /// <item>User has not confirmed the email</item>
        /// </list>
        /// </summary>
        /// <returns>True if the notification should be sent, false otherwise.</returns>
        protected static bool ShouldSendNotification(UserManager<ApplicationUser> userManager, ApplicationUser user)
        {
            return !userManager.IsInRole(user.Id, ConfigurationManager.AppSettings.Get("UnsubscribedUsersRole")) && user.EmailConfirmed;
        }
    }
}
