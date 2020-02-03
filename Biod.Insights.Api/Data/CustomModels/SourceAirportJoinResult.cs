using Biod.Insights.Api.Data.EntityModels;

namespace Biod.Insights.Api.Data.CustomModels
{
    public class SourceAirportJoinResult
    {
        public EventSourceAirport SourceAirport { get; set; }
        
        public Geonames City { get; set; }
    }
}