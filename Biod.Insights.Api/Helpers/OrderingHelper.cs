using System.Collections.Generic;
using System.Linq;
using Biod.Insights.Api.Constants;
using Biod.Insights.Api.Models.Event;

namespace Biod.Insights.Api.Helpers
{
    public static class OrderingHelper
    {
        public static IEnumerable<GetEventModel> OrderEventsByDefault(IEnumerable<GetEventModel> events)
        {
            return events
                .OrderByDescending(e => e.IsLocal)
                .ThenByDescending(e => e.ImportationRisk?.MaxProbability)
                .ThenByDescending(e => e.ImportationRisk?.MaxMagnitude)
                .ThenByDescending(e => e.ExportationRisk.MaxProbability)
                .ThenByDescending(e => e.ExportationRisk.MaxMagnitude);
        }

        public static IEnumerable<EventLocationModel> OrderLocationsByDefault(IEnumerable<EventLocationModel> locations)
        {
            var locationTypePreference = new List<int> { (int) LocationType.Country, (int) LocationType.Province, (int) LocationType.City };
            return locations
                .OrderBy(e => locationTypePreference.IndexOf(e.LocationType))
                .ThenBy(e => e.ProvinceName)
                .ThenBy(e => e.LocationName);
        }
    }
}