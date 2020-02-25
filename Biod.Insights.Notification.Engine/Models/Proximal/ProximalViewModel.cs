using Biod.Insights.Common.Constants;

namespace Biod.Insights.Notification.Engine.Models.Proximal
{
    public class ProximalViewModel : EmailViewModel
    {
        public override NotificationType NotificationType => NotificationType.Proximal;

        public override string ViewFileName => "ProximalNotification";

        public string DiseaseName { get; set; }
        
        public string EventLocationName { get; set; } 
        
        public LocationType EventLocationType { get; set; }

        public string LastUpdateDate { get; set; }
    }
}