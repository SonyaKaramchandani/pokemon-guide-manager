using System;

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

    }
}
