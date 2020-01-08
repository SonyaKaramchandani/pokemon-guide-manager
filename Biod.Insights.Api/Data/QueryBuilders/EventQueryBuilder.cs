using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.CustomModels;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Interface;
using Microsoft.EntityFrameworkCore;

namespace Biod.Insights.Api.Data.QueryBuilders
{
    public class EventQueryBuilder : IQueryBuilder<EventJoinResult>
    {
        [NotNull] private readonly BiodZebraContext _dbContext;
        
        private int? _eventId;
        private int? _diseaseId;
        private int? _geonameId;

        private bool _includeArticles;
        private bool _includeLocations;
        private bool _includeExportationRisk;
        private bool _includeImportationRisk;

        public EventQueryBuilder([NotNull] BiodZebraContext dbContext)
        {
            _dbContext = dbContext;
        }

        public EventQueryBuilder SetEventId(int eventId)
        {
            _eventId = eventId;
            return this;
        }

        public EventQueryBuilder SetDiseaseId(int diseaseId)
        {
            _diseaseId = diseaseId;
            return this;
        }

        public EventQueryBuilder IncludeAll(int? geonameId)
        {
            var query = this
                .IncludeArticles()
                .IncludeLocations()
                .IncludeExportationRisk();

            if (geonameId.HasValue)
            {
                query = query.IncludeImportationRisk(geonameId.Value);
            }

            return query;
        }

        public EventQueryBuilder IncludeArticles()
        {
            _includeArticles = true;
            return this;
        }

        public EventQueryBuilder IncludeLocations()
        {
            _includeLocations = true;
            return this;
        }

        public EventQueryBuilder IncludeExportationRisk()
        {
            _includeExportationRisk = true;
            return this;
        }

        public EventQueryBuilder IncludeImportationRisk(int geonameId)
        {
            _includeImportationRisk = true;
            _geonameId = geonameId;
            return this;
        }

        public async Task<IEnumerable<EventJoinResult>> BuildAndExecute()
        {
            var query = _dbContext.Event
                .Where(e => e.EndDate == null)
                .AsQueryable();

            if (_eventId != null)
            {
                query = query.Where(e => e.EventId == _eventId);
            }

            if (_diseaseId != null)
            {
                query = query.Where(e => e.DiseaseId == _diseaseId);
            }
            
            if (_includeArticles)
            {
                query = query
                    .Include(e => e.XtblArticleEvent)
                    .ThenInclude(x => x.Article)
                    .ThenInclude(a => a.ArticleFeed);
            }

            if (_includeLocations)
            {
                query = query
                    .Include(e => e.XtblEventLocation)
                    .ThenInclude(x => x.Geoname);
            }
            
            // Queries involving joining
            var joinQuery = query.Select(e => new EventJoinResult {Event = e});
            
            if (_includeExportationRisk)
            {
                joinQuery = 
                    from e in joinQuery
                    join a in _dbContext.EventDestinationAirport.Where(a => a.DestinationStationId == -1) on e.Event.EventId equals a.EventId into ea
                    from a in ea.DefaultIfEmpty()
                    select new EventJoinResult {Event = e.Event, ExportationRisk = a};
            }

            if (_includeImportationRisk && _geonameId.HasValue)
            {
                joinQuery =
                    from e in joinQuery
                    join r in _dbContext.EventImportationRisksByGeoname.Where(r => r.GeonameId == _geonameId.Value) on e.Event.EventId equals r.EventId into er
                    from r in er.DefaultIfEmpty()
                    select new EventJoinResult { Event = e.Event, ExportationRisk = e.ExportationRisk, ImportationRisk = r};
            }

            return await joinQuery.ToListAsync();
        }
    }
}