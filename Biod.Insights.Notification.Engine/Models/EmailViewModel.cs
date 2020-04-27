using Biod.Products.Common.Constants;

namespace Biod.Insights.Notification.Engine.Models
{
    public abstract class EmailViewModel : NotificationViewModel
    {
        public abstract override NotificationType NotificationType { get; }
        
        public bool IsEmailTestingEnabled { get; set; }

        public string AoiGeonameIds { get; set; }

        public string Email { get; set; }
    }
}