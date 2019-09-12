using System;

namespace Biod.Zebra.Library.Models.Notification
{
    public interface INotificationViewModel
    {
        string UserId { get; set; }

        int? EventId { get; set; }

        string Title { get; set; }

        DateTimeOffset SentDate { get; set; }

        int NotificationType { get; }

        bool DoNotTrackEnabled { get; set; }
        bool EmailConfirmed { get; set; }
    }
}
