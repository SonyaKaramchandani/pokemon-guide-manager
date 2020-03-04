using System.Collections.Generic;
using Biod.Insights.Common.Constants;

namespace Biod.Insights.Notification.Engine.Models.Weekly
{
    public class WeeklyViewModel : EmailViewModel
    {
        public WeeklyViewModel()
        {
            Title = "Weekly Brief";
        }
        
        public override NotificationType NotificationType => NotificationType.WeeklyBrief;

        public string DateRange { get; set; }
        
        public IEnumerable<WeeklyLocationViewModel> UserLocations { get; set; }
    }
}