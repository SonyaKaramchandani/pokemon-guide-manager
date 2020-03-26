using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Biod.Insights.Service.Data.QueryBuilders
{
    public class SourceAirportQueryBuilder : IQueryBuilder<EventSourceAirportSpreadMd, SourceAirportJoinResult>
    {
        [NotNull] private readonly BiodZebraContext _dbContext;
        [NotNull] private readonly SourceAirportConfig _config;

        private IQueryable<EventSourceAirportSpreadMd> _customInitialQueryable;

        public SourceAirportQueryBuilder([NotNull] BiodZebraContext dbContext) : this(dbContext,
            new SourceAirportConfig.Builder().Build())
        {
            
        }
        
        public SourceAirportQueryBuilder([NotNull] BiodZebraContext dbContext, [NotNull] SourceAirportConfig config)
        {
            _customInitialQueryable = null;
            _dbContext = dbContext;
            _config = config;
        }

        public IQueryable<EventSourceAirportSpreadMd> GetInitialQueryable()
        {
            return _customInitialQueryable ?? _dbContext.EventSourceAirportSpreadMd.AsQueryable();
        }

        public IQueryBuilder<EventSourceAirportSpreadMd, SourceAirportJoinResult> OverrideInitialQueryable(IQueryable<EventSourceAirportSpreadMd> customQueryable)
        {
            _customInitialQueryable = customQueryable;
            return this;
        }
        
        public async Task<IEnumerable<SourceAirportJoinResult>> BuildAndExecute()
        {
            var query = GetInitialQueryable();

            if (_config.EventId != null)
            {
                query = query.Where(a => a.EventId == _config.EventId);
            }

            if (_config.IncludeEventInformation)
            {
                query = query.Include(a => a.Event);
            }

            if (_config.IncludeAirportInformation)
            {
                query = query.Include(a => a.SourceStation);

                var joinQuery =
                    from a in query
                    join g in _dbContext.Geonames on a.SourceStation.CityGeonameId equals g.GeonameId into ag
                    from g in ag.DefaultIfEmpty()
                    select new SourceAirportJoinResult {SourceAirport = a, City = g};

                return await joinQuery.ToListAsync();
            }

            return await query
                .Select(a => new SourceAirportJoinResult {SourceAirport = a})
                .ToListAsync();
        }
    }
}