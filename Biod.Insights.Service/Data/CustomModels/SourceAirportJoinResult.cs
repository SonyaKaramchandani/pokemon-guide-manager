using Biod.Insights.Data.EntityModels;

namespace Biod.Insights.Service.Data.CustomModels
{
    public class SourceAirportJoinResult
    {
        public EventSourceAirportSpreadMd SourceAirport { get; set; }
        
        public Geonames City { get; set; }
    }
}