using System.Collections.Generic;

namespace Biod.Insights.Api.Models.Map
{
    public class EventsPinModel
    {
        public int GeonameId { get; set; }
        
        public string LocationName { get; set; }
        
        public string Point { get; set; }
        
        public List<int> EventIds { get; set; }
    }
}