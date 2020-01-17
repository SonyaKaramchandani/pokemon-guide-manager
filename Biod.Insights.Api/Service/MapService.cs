using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models.Event;
using Biod.Insights.Api.Models.Map;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<EventsPinModel>> GetCountryEventPins()
        {
            return await GetCountryEventPins(null);
        }

        public async Task<IEnumerable<EventsPinModel>> GetCountryEventPins([AllowNull] HashSet<int> eventIds)
        {
            var query = _biodZebraContext.XtblEventLocation
                .Where(x => x.Event.EndDate == null);

            if (eventIds != null)
            {
                query = query
                    .Include(x => x.Event)
                    .Where(x => eventIds.Contains(x.EventId));
            }

            return (await query
                    .Select(x => new {x.Geoname.CountryGeonameId, x.Event})
                    .Distinct()
                    .Join(
                        _biodZebraContext.Geonames,
                        g => g.CountryGeonameId,
                        c => c.GeonameId,
                        (g, c) => new {@event = g.Event, country = c})
                    .ToListAsync())
                .GroupBy(g => g.country.GeonameId)
                .Select(g => new EventsPinModel
                {
                    GeonameId = g.Key,
                    LocationName = g.First().country.CountryName,
                    Point = g.First().country.Shape.ToText(),
                    Events = g.Select(o => new EventInformationModel
                    {
                        Id = o.@event.EventId,
                        Summary = o.@event.Summary,
                        Title = o.@event.EventTitle,
                        DiseaseId = o.@event.DiseaseId,
                        StartDate = o.@event.StartDate,
                        EndDate = o.@event.EndDate,
                        LastUpdatedDate = o.@event.LastUpdatedDate
                    }).ToList()
                });
        }
    }
}