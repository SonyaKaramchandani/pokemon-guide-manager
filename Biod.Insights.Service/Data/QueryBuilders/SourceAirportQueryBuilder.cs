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
        [NotNull] private readonly AirportConfig _config;

        private IQueryable<EventSourceAirportSpreadMd> _customInitialQueryable;

        public SourceAirportQueryBuilder([NotNull] BiodZebraContext dbContext, [NotNull] AirportConfig config)
        {
            _customInitialQueryable = null;
            _dbContext = dbContext;
            _config = config;
        }

        public IQueryable<EventSourceAirportSpreadMd> GetInitialQueryable()
        {
            return _customInitialQueryable ?? _dbContext.EventSourceAirportSpreadMd.AsQueryable().AsNoTracking();
        }

        public IQueryBuilder<EventSourceAirportSpreadMd, SourceAirportJoinResult> OverrideInitialQueryable(IQueryable<EventSourceAirportSpreadMd> customQueryable)
        {
            _customInitialQueryable = customQueryable;
            return this;
        }

        public async Task<IEnumerable<SourceAirportJoinResult>> BuildAndExecute()
        {
            var query = GetInitialQueryable().Where(a => a.EventId == _config.EventId);

            return await query
                .Select(a => new SourceAirportJoinResult
                {
                    StationId = a.SourceStationId,
                    StationCode = a.SourceStation.StationCode,
                    StationName = a.SourceStation.StationGridName,
                    Latitude = (float) (a.SourceStation.Latitude ?? 0),
                    Longitude = (float) (a.SourceStation.Longitude ?? 0),
                    Volume = a.Volume ?? 0,
                    CityGeonameId = _config.IncludeCity ? a.SourceStation.CityGeonameId : null,
                    CityName = _config.IncludeCity ? a.SourceStation.CityGeoname.DisplayName : null
                })
                .ToListAsync();
        }
    }
}