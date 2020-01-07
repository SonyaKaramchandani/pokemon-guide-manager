using Biod.Insights.Api.Data.EntityModels;

namespace Biod.Insights.Api.Data.CustomModels
{
    public class DestinationAirportJoinResult
    {
        public EventDestinationAirport DestinationAirport { get; set; }
        
        public Stations Station { get; set; }
        
        public Geonames City { get; set; }
    }
}