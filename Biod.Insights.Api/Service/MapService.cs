using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models.Map;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Service
{
    public class MapService : IMapService
    {
        private readonly ILogger<MapService> _logger;
        private readonly BiodZebraContext _biodZebraContext;

        /// <summary>
        /// Map service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        public MapService(BiodZebraContext biodZebraContext, ILogger<MapService> logger)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
        }
        
        public IEnumerable<EventsPinModel> GetCountryEventPins()
        {
            return GetCountryEventPins(null);
        }

        public IEnumerable<EventsPinModel> GetCountryEventPins([AllowNull] HashSet<int> eventIds)
        {
            var query = _biodZebraContext.XtblEventLocation
                .Where(x => x.Event.EndDate == null);

            if (eventIds != null)
            {
                query = query.Where(x => eventIds.Contains(x.EventId));
            }
                
            return query
                .Select(x => new {x.Geoname.CountryGeonameId, x.EventId})
                .Distinct()
                .Join(
                    _biodZebraContext.Geonames,
                    g => g.CountryGeonameId,
                    c => c.GeonameId,
                    (g, c) => new { @event = g.EventId, country = c})
                .ToList()
                .GroupBy(g => g.country.GeonameId)
                .Select(g => new EventsPinModel
                {
                    GeonameId = g.Key,
                    LocationName = g.First().country.CountryName,
                    Point = g.First().country.Shape.ToText(),
                    EventIds = g.Select(o => o.@event).ToList()
                });
        }
    }
}