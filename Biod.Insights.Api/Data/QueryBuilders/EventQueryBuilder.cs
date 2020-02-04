using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.CustomModels;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Helpers;
using Biod.Insights.Api.Interface;
using Microsoft.EntityFrameworkCore;

namespace Biod.Insights.Api.Data.QueryBuilders
{
    public class EventQueryBuilder : IQueryBuilder<Event, EventJoinResult>
    {
        [NotNull] private readonly BiodZebraContext _dbContext;

        private int? _eventId;
        private int? _geonameId;
        private readonly HashSet<int> _diseaseIds = new HashSet<int>();

        private bool _includeArticles;
        private bool _includeLocations;
        private bool _includeExportationRisk;
        private bool _includeImportationRisk;

        public EventQueryBuilder([NotNull] BiodZebraContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Event> GetInitialQueryable()
        {
            return _dbContext.Event
                .Where(e => e.EndDate == null)
                .AsQueryable();
        }

        public EventQueryBuilder SetEventId(int eventId)
        {
            _eventId = eventId;
            return this;
        }

        public EventQueryBuilder AddDiseaseId(int diseaseId)
        {
            _diseaseIds.Add(diseaseId);
            return this;
        }
        
        public EventQueryBuilder AddDiseaseIds(IEnumerable<int> diseaseIds)
        {
            _diseaseIds.UnionWith(diseaseIds);
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
            var query = GetInitialQueryable();

            if (_eventId != null)
            {
                query = query.Where(e => e.EventId == _eventId);
            }

            if (_diseaseIds.Any())
            {
                query = query.Where(e => _diseaseIds.Contains(e.DiseaseId));
            }

            if (_includeExportationRisk)
            {
                query = query.Include(e => e.EventExtension);
            }

            // Queries involving joining
            var joinQuery = query.Select(e => new EventJoinResult {Event = e});

            if (_includeImportationRisk && _geonameId.HasValue)
            {
                joinQuery =
                    from e in joinQuery
                    join r in _dbContext.EventImportationRisksByGeoname.Where(r => r.GeonameId == _geonameId.Value) on e.Event.EventId equals r.EventId into er
                    from r in er.DefaultIfEmpty()
                    select new EventJoinResult {Event = e.Event, ImportationRisk = r};
            }

            var executedResult = await joinQuery.ToListAsync();
            var allEventIds = new HashSet<int>(executedResult.Select(e => e.Event.EventId));

            // Queries that avoid joining due to large data set in other table
            if (_includeLocations)
            {
                var locationLookup = (
                        from x in _dbContext.XtblEventLocation.Where(x => allEventIds.Contains(x.EventId))
                        join g in _dbContext.Geonames on x.GeonameId equals g.GeonameId
                        select new XtblEventLocationJoinResult
                        {
                            EventDate = x.EventDate,
                            EventId = x.EventId,
                            ConfCases = x.ConfCases ?? 0,
                            SuspCases = x.SuspCases ?? 0,
                            RepCases = x.RepCases ?? 0,
                            Deaths = x.Deaths ?? 0,
                            GeonameId = g.GeonameId,
                            GeonameDisplayName = g.DisplayName,
                            Admin1GeonameId = g.Admin1GeonameId,
                            Admin1Name = g.Admin1Geoname.Name,
                            CountryGeonameId = g.CountryGeonameId ?? -1,
                            CountryName = g.CountryGeoname.Name,
                            LocationType = g.LocationType
                        }
                    )
                    .AsEnumerable()
                    .GroupBy(o => o.EventId)
                    .ToList();

                foreach (var e in executedResult)
                {
                    e.XtblEventLocations = locationLookup.FirstOrDefault(l => l.Key == e.Event.EventId)?.ToList() ?? new List<XtblEventLocationJoinResult>();
                }
            }

            if (_includeArticles && _eventId == null)
            {
                var articleLookup = (
                        from x in _dbContext.XtblArticleEvent.Where(x => allEventIds.Contains(x.EventId))
                        join a in _dbContext.ProcessedArticle on x.ArticleId equals a.ArticleId
                        join f in _dbContext.ArticleFeed on a.ArticleFeedId equals f.ArticleFeedId
                        select new XtblEventArticleJoinResult
                        {
                            EventId = x.EventId,
                            ArticleId = a.ArticleId,
                            ArticleFeedId = a.ArticleFeedId,
                            ArticleTitle = a.ArticleTitle,
                            OriginalLanguage = a.OriginalLanguage,
                            OriginalSourceUrl = a.OriginalSourceUrl,
                            FeedUrl = a.FeedUrl,
                            FeedPublishedDate = a.FeedPublishedDate,
                            DisplayName = f.DisplayName,
                            SeqId = f.SeqId
                        }
                    )
                    .AsEnumerable()
                    .GroupBy(a => a.EventId)
                    .ToList();
                
                foreach (var e in executedResult)
                {
                    e.ArticleSources = articleLookup.FirstOrDefault(l => l.Key == e.Event.EventId)?
                        .Select(a => new usp_ZebraEventGetArticlesByEventId_Result
                        {
                            ArticleTitle = a.ArticleTitle,
                            OriginalLanguage = a.OriginalLanguage,
                            OriginalSourceURL = a.OriginalSourceUrl,
                            FeedURL = a.FeedUrl,
                            FeedPublishedDate = a.FeedPublishedDate,
                            DisplayName = ArticleHelper.GetDisplayName(a.ArticleFeedId, a.OriginalSourceUrl, a.DisplayName),
                            SeqId = ArticleHelper.GetSeqId(a.ArticleFeedId, a.OriginalSourceUrl, a.SeqId)
                        });
                }
            }
            else if (_includeArticles)
            {
                executedResult = executedResult
                    .Select(e =>
                    {
                        e.ArticleSources = _dbContext.usp_ZebraEventGetArticlesByEventId_Result
                            .FromSqlInterpolated($@"EXECUTE zebra.usp_ZebraEventGetArticlesByEventId
                                            @EventId = {e.Event.EventId}")
                            .ToList();
                        return e;
                    })
                    .ToList();
            }

            return executedResult;
        }
    }
}