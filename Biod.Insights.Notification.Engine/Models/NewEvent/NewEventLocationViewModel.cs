using Biod.Insights.Service.Models;

namespace Biod.Insights.Notification.Engine.Models.NewEvent
{
    public class NewEventLocationViewModel
    {
        public int GeonameId { get; set; }
        
        public string LocationName { get; set; }
        
        public int LocationType { get; set; }
        
        public bool IsLocal { get; set; }
        
        public int OutbreakPotentialCategoryId { get; set; }
        
        public RiskModel ImportationRisk { get; set; }
    }
}