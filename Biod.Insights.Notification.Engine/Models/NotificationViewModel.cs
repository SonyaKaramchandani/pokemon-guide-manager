using System;
using Biod.Insights.Common.Constants;

namespace Biod.Insights.Notification.Engine.Models
{
    public abstract class NotificationViewModel
    {
        public abstract NotificationType NotificationType { get; }
        
        public string UserId { get; set; }

        public int? EventId { get; set; }

        public string Title { get; set; }

        public DateTimeOffset SentDate { get; set; }

        public bool DoNotTrackEnabled { get; set; }

        public bool EmailConfirmed { get; set; }
    }
}