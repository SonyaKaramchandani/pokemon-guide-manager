using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Biod.Products.Common.Constants;
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

        public SourceAirportQueryBuilder([NotNull] BiodZebraContext dbContext, [NotNull] SourceAirportConfig config)
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
                    IsModelNotRun = a.Event.IsLocalOnly || a.Event.XtblEventLocation.All(el => el.Geoname.LocationType == (int) LocationType.Country),
                    StationId = a.SourceStationId,
                    StationCode = a.SourceStation.StationCode,
                    StationName = a.SourceStation.StationGridName,
                    Latitude = (float) (a.SourceStation.Latitude ?? 0),
                    Longitude = (float) (a.SourceStation.Longitude ?? 0),
                    Volume = a.Volume ?? 0,
                    MaxProb = (float) (a.MaxProb ?? 0),
                    MinProb = (float) (a.MinProb ?? 0),
                    MaxExpVolume = (float) (a.MaxExpVolume ?? 0),
                    MinExpVolume = (float) (a.MinExpVolume ?? 0),
                    MinPrevalence = a.MinPrevalence ?? 0,
                    MaxPrevalence = a.MaxPrevalence ?? 0,
                    CityGeonameId = _config.IncludeCity ? a.SourceStation.CityGeonameId : null,
                    CityName = _config.IncludeCity ? a.SourceStation.CityGeoname.DisplayName : null,
                    Population = _config.IncludePopulation
                        ? (int) Math.Round(
                            a.SourceStation.GridStation
                                .Where(s => s.ValidFromDate.Month == DateTime.Today.Month)
                                .Sum(s => (double) (s.Probability ?? 0) * (s.Grid.Population ?? 0)))
                        : 0,
                    GridStationCases = _config.IncludeCaseCounts
                        ? (
                            from sg in _dbContext.EventSourceGridSpreadMd.Where(e => e.EventId == _config.EventId)
                            join gs in _dbContext.GridStation.Where(s => s.StationId == a.SourceStationId && s.ValidFromDate.Month == DateTime.Today.Month) on sg.GridId equals gs.GridId
                            select new GridStationCaseJoinResult
                            {
                                Cases = sg.Cases,
                                MinCases = sg.MinCases,
                                MaxCases = sg.MaxCases,
                                Probability = (double) (gs.Probability ?? 0)
                            })
                        : null
                })
                .ToListAsync();
        }
    }
}