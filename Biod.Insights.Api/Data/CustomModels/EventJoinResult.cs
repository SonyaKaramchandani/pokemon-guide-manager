using System.Collections.Generic;
using Biod.Insights.Api.Data.EntityModels;

namespace Biod.Insights.Api.Data.CustomModels
{
    public class EventJoinResult
    {
        public Event Event { get; set; }
        
        public EventDestinationAirport ExportationRisk { get; set; }
        
        public EventImportationRisksByGeoname ImportationRisk { get; set; }
        
        /// <summary>
        /// Lookup for full geonames details referenced by Event Locations' Province and Country
        /// </summary>
        public Dictionary<int, Geonames> GeonamesLookup { get; set; }
    }
}