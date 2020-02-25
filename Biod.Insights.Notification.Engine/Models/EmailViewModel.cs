using Biod.Insights.Common.Constants;

namespace Biod.Insights.Notification.Engine.Models
{
    public abstract class EmailViewModel : NotificationViewModel
    {
        public abstract override NotificationType NotificationType { get; }

        public abstract string ViewFileName { get; }

        public string AoiGeonameIds { get; set; }

        public string Email { get; set; }
    }
}