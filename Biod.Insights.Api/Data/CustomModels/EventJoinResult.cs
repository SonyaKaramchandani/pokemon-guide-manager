using Biod.Insights.Api.Data.EntityModels;

namespace Biod.Insights.Api.Data.CustomModels
{
    public class EventJoinResult
    {
        public Event Event { get; set; }
        
        public EventDestinationAirport ExportationRisk { get; set; }
        
        public EventImportationRisksByGeoname ImportationRisk { get; set; }
    }
}