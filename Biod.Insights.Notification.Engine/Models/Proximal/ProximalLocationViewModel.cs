using Biod.Insights.Service.Models;

namespace Biod.Insights.Notification.Engine.Models.Proximal
{
    public class ProximalEventLocationViewModel
    {
        public int GeonameId  { get; set; }
        
        public string LocationName { get; set; }
        
        public int LocationType { get; set; }
        
        public CaseCountModel CaseCountChange { get; set; }
    }
}