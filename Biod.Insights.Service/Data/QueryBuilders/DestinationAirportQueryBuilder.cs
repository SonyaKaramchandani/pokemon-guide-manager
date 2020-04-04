using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Common.Constants;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Biod.Insights.Service.Data.QueryBuilders
{
    public class DestinationAirportQueryBuilder : IQueryBuilder<EventDestinationAirportSpreadMd, AirportJoinResult>
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
                .Where(a => a.DestinationStationId > 0) // -1 is an legacy aggregate
                .AsNoTracking();
        }

        public IQueryBuilder<EventDestinationAirportSpreadMd, AirportJoinResult> OverrideInitialQueryable(IQueryable<EventDestinationAirportSpreadMd> customQueryable)
        {
            _customInitialQueryable = customQueryable;
            return this;
        }

        public async Task<IEnumerable<AirportJoinResult>> BuildAndExecute()
        {
            var query = GetInitialQueryable().Where(a => a.EventId == _config.EventId);

            if (_config.IncludeImportationRisk && _config.GeonameId.HasValue)
            {
                // Only include airports that are relevant to the geoname id provided
                var gridIdsQuery = SqlQuery.GetGridIdsByGeoname(_dbContext, _config.GeonameId.Value, out var isEnumerated);
                var gridIds = isEnumerated ? gridIdsQuery.ToList() : new List<string>();
                var catchmentThreshold = Convert.ToDecimal(SqlQuery.GetConfigurationVariable(_dbContext, nameof(ConfigurationVariableName.DestinationCatchmentThreshold), 0.1));

                query = query
                    .Where(a => a.DestinationStation.GridStation
                        .Any(gs =>
                            gs.ValidFromDate.Month == DateTime.Today.Month // GridStation depends on monthly data
                            && (gs.Probability ?? 0) >= catchmentThreshold // Probability must be greater than catchment threshold
                            && (isEnumerated // Use the enumerable if already enumerated, otherwise leave as query
                                ? gridIds.Contains(gs.GridId)
                                : gridIdsQuery.Contains(gs.GridId))));
            }

            return await query
                .Select(a => new AirportJoinResult
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