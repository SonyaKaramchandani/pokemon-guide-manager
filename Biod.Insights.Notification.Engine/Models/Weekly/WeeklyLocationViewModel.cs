using System.Collections.Generic;

namespace Biod.Insights.Notification.Engine.Models.Weekly
{
    public class WeeklyLocationViewModel
    {
        public int GeonameId { get; set; }
        
        public string LocationName { get; set; }
        
        public int LocationType { get; set; }
        
        public IEnumerable<WeeklyEventViewModel> Events { get; set; }
        
        public int TotalEvents { get; set; } 
    }
}