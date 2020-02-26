using System.Collections.Generic;
using Biod.Insights.Service.Models.Airport;

namespace Biod.Insights.Service.Models.Event
{
    public class EventAirportModel
    {
        public IEnumerable<GetAirportModel> SourceAirports { get; set; }
        
        public IEnumerable<GetAirportModel> DestinationAirports { get; set; }
    }
}