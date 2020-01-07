using System.Collections.Generic;
using Biod.Insights.Api.Models.Airport;

namespace Biod.Insights.Api.Models.Event
{
    public class EventAirportModel
    {
        public IEnumerable<GetAirportModel> SourceAirports { get; set; }
        
        public IEnumerable<GetAirportModel> DestinationAirports { get; set; }
    }
}