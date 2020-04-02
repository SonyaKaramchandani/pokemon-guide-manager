using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Data.CustomModels;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Helpers;
using Biod.Insights.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Biod.Insights.Common.Constants;
using Biod.Insights.Service.Configs;

namespace Biod.Insights.Service.Data.QueryBuilders
{
    public class EventQueryBuilder : IQueryBuilder<Event, EventJoinResult>
    {
        [NotNull] private readonly BiodZebraContext _dbContext;
        [NotNull] private readonly EventConfig _eventConfig;

        private IQueryable<Event> _customInitialQueryable;

        public EventQueryBuilder([NotNull] BiodZebraContext dbContext) : this(dbContext, new EventConfig.Builder().Build())
        {
        }

        public EventQueryBuilder([NotNull] BiodZebraContext dbContext, [NotNull] EventConfig eventConfig)
        {
            _dbContext = dbContext;
            _eventConfig = eventConfig;
        }

        public IQueryable<Event> GetInitialQueryable()
        {
            return _customInitialQueryable ?? _dbContext.Event
                .Where(e => e.EndDate == null)
                .AsQueryable()
                .AsNoTracking();
        }

        public IQueryBuilder<Event, EventJoinResult> OverrideInitialQueryable(IQueryable<Event> customQueryable)
        {
            _customInitialQueryable = customQueryable;
            return this;
        }

        public async Task<IEnumerable<EventJoinResult>> BuildAndExecute()
        {
            var query = GetInitialQueryable();

            if (_eventConfig.EventId.HasValue)
            {
                query = query.Where(e => e.EventId == _eventConfig.EventId.Value);
            }

            if (_eventConfig.DiseaseIds.Any())
            {
                query = query.Where(e => _eventConfig.DiseaseIds.Contains(e.DiseaseId));
            }

            if (_eventConfig.IncludeExportationRisk)
            {
                query = query.Include(e => e.EventExtensionSpreadMd);
            }

            if (_eventConfig.IncludeCalculationMetadata)
            {
                query = query.Include(e => e.EventSourceGridSpreadMd);
            }

            // Queries involving joining
            var joinQuery = query.Select(e => new EventJoinResult {Event = e});

            if (_eventConfig.IncludeImportationRisk && _eventConfig.GeonameId.HasValue)
            {
                joinQuery =
                    from e in joinQuery
                    join r in _dbContext.EventImportationRisksByGeonameSpreadMd.Where(r => r.GeonameId == _eventConfig.GeonameId.Value) on e.Event.EventId equals r.EventId into er
                    from r in er.DefaultIfEmpty()
                    select new EventJoinResult {Event = e.Event, ImportationRisk = r};
            }

            var executedResult = await joinQuery.ToListAsync();
            var allEventIds = new HashSet<int>(executedResult.Select(e => e.Event.EventId));

            // Queries that avoid joining due to large data set in other table
            if (_eventConfig.IncludeLocations)
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

            if (_eventConfig.IncludeLocationsHistory)
            {
                var locationLookup = (
                        from x in _dbContext.XtblEventLocationHistory.Where(x => allEventIds.Contains(x.EventId) && x.EventDateType == (int) EventLocationHistoryDateType.Proximal)
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
                    e.XtblEventLocationsHistory = locationLookup.FirstOrDefault(l => l.Key == e.Event.EventId)?.ToList() ?? new List<XtblEventLocationJoinResult>();
                }
            }

            if (_eventConfig.IncludeArticles && !_eventConfig.EventId.HasValue)
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
            else if (_eventConfig.IncludeArticles)
            {
                executedResult = executedResult
                    .Select(e =>
                    {
                        e.ArticleSources = SqlQuery.GetArticlesByEvent(_dbContext, e.Event.EventId);
                        return e;
                    })
                    .ToList();
            }

            return executedResult;
        }
    }
}