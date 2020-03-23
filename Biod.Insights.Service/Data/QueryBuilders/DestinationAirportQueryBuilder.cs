using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Biod.Insights.Service.Data.QueryBuilders
{
    public class DestinationAirportQueryBuilder : IQueryBuilder<EventDestinationAirportSpreadMd, DestinationAirportJoinResult>
    {
        [NotNull] private readonly BiodZebraContext _dbContext;

        private IQueryable<EventDestinationAirportSpreadMd> _customInitialQueryable;
        private int? _eventId;

        public DestinationAirportQueryBuilder([NotNull] BiodZebraContext dbContext)
        {
            _customInitialQueryable = null;
            _dbContext = dbContext;
        }

        public IQueryable<EventDestinationAirportSpreadMd> GetInitialQueryable()
        {
            return _customInitialQueryable ?? _dbContext.EventDestinationAirportSpreadMd
                       .Where(a => a.DestinationStationId > 0); // -1 is an legacy aggregate
        }

        public IQueryBuilder<EventDestinationAirportSpreadMd, DestinationAirportJoinResult> OverrideInitialQueryable(IQueryable<EventDestinationAirportSpreadMd> customQueryable)
        {
            _customInitialQueryable = customQueryable;
            return this;
        }

        public DestinationAirportQueryBuilder SetEventId(int eventId)
        {
            _eventId = eventId;
            return this;
        }

        public async Task<IEnumerable<DestinationAirportJoinResult>> BuildAndExecute()
        {
            var query = GetInitialQueryable();

            if (_eventId != null)
            {
                query = query.Where(a => a.EventId == _eventId);
            }

            return await query.Select(a => new DestinationAirportJoinResult
            {
                StationId = a.DestinationStationId,
                StationName = a.StationName,
                StationCode = a.StationCode,
                Latitude = (float) (a.Latitude ?? 0),
                Longitude = (float) (a.Longitude ?? 0),
                Volume = a.Volume ?? 0,
                MaxProb = (float) (a.MaxProb ?? 0),
                MinProb = (float) (a.MinProb ?? 0),
                MaxExpVolume = (float) (a.MaxExpVolume ?? 0),
                MinExpVolume = (float) (a.MinExpVolume ?? 0),
                CityName = a.DestinationStation.CityGeoname.DisplayName
            }).ToListAsync();
        }
    }
}