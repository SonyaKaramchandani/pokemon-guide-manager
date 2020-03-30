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
    public class DestinationAirportQueryBuilder : IQueryBuilder<EventDestinationAirportSpreadMd, DestinationAirportJoinResult>
    {
        [NotNull] private readonly BiodZebraContext _dbContext;
        [NotNull] private readonly AirportConfig _config;

        private IQueryable<EventDestinationAirportSpreadMd> _customInitialQueryable;

        public DestinationAirportQueryBuilder([NotNull] BiodZebraContext dbContext, [NotNull] AirportConfig config)
        {
            _customInitialQueryable = null;
            _dbContext = dbContext;
            _config = config;
        }

        public IQueryable<EventDestinationAirportSpreadMd> GetInitialQueryable()
        {
            return _customInitialQueryable ?? _dbContext.EventDestinationAirportSpreadMd
                .Where(a => a.DestinationStationId > 0)
                .AsNoTracking(); // -1 is an legacy aggregate
        }

        public IQueryBuilder<EventDestinationAirportSpreadMd, DestinationAirportJoinResult> OverrideInitialQueryable(IQueryable<EventDestinationAirportSpreadMd> customQueryable)
        {
            _customInitialQueryable = customQueryable;
            return this;
        }

        public async Task<IEnumerable<DestinationAirportJoinResult>> BuildAndExecute()
        {
            var query = GetInitialQueryable().Where(a => a.EventId == _config.EventId);

            return await query
                .Select(a => new DestinationAirportJoinResult
                {
                    IsModelNotRun = a.Event.IsLocalOnly,
                    StationId = a.DestinationStationId,
                    StationName = a.DestinationStation.StationGridName,
                    StationCode = a.DestinationStation.StationCode,
                    Latitude = (float) (a.Latitude ?? 0),
                    Longitude = (float) (a.Longitude ?? 0),
                    Volume = a.Volume ?? 0,
                    MaxProb = (float) (a.MaxProb ?? 0),
                    MinProb = (float) (a.MinProb ?? 0),
                    MaxExpVolume = (float) (a.MaxExpVolume ?? 0),
                    MinExpVolume = (float) (a.MinExpVolume ?? 0),
                    CityGeonameId = _config.IncludeCity ? a.DestinationStation.CityGeonameId : null,
                    CityName = _config.IncludeCity ? a.DestinationStation.CityGeoname.DisplayName : null
                })
                .ToListAsync();
        }
    }
}