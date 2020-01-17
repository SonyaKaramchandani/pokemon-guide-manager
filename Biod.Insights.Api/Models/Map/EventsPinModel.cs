using System.Collections.Generic;
using Biod.Insights.Api.Models.Event;

namespace Biod.Insights.Api.Models.Map
{
    public class EventsPinModel
    {
        public int GeonameId { get; set; }
        
        public string LocationName { get; set; }
        
        public string Point { get; set; }
        
        public List<EventInformationModel> Events { get; set; }
    }
}