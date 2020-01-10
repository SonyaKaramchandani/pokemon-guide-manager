using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.CustomModels;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models.Geoname;
using Microsoft.EntityFrameworkCore;

namespace Biod.Insights.Api.Data.QueryBuilders
{
    public class DestinationAirportQueryBuilder : IQueryBuilder<EventDestinationAirport, DestinationAirportJoinResult>
    {
        [NotNull] private readonly BiodZebraContext _dbContext;

        private int? _eventId;
        private GetGeonameModel _geoname;

        private bool _includeAirportInformation;
        private bool _includeEventInformation;

        public DestinationAirportQueryBuilder([NotNull] BiodZebraContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<EventDestinationAirport> GetInitialQueryable()
        {
            return _dbContext.EventDestinationAirport
                .Where(a => a.DestinationStationId > 0); // -1 is an aggregate
        }

        public DestinationAirportQueryBuilder SetEventId(int eventId)
        {
            _eventId = eventId;
            return this;
        }

        public DestinationAirportQueryBuilder SetGeoname(GetGeonameModel geoname)
        {
            _geoname = geoname;
            return this;
        }

        public DestinationAirportQueryBuilder IncludeAll()
        {
            return this
                .IncludeAirportInformation()
                .IncludeEventInformation();
        }

        public DestinationAirportQueryBuilder IncludeAirportInformation()
        {
            _includeAirportInformation = true;
            return this;
        }

        public DestinationAirportQueryBuilder IncludeEventInformation()
        {
            _includeEventInformation = true;
            return this;
        }

        public async Task<IEnumerable<DestinationAirportJoinResult>> BuildAndExecute()
        {
            var query = GetInitialQueryable();

            if (_eventId != null)
            {
                query = query.Where(a => a.EventId == _eventId);
            }

            if (_includeEventInformation)
            {
                query = query.Include(a => a.Event);
            }

            if (_includeAirportInformation)
            {
                var joinQuery =
                    from a in query
                    join s in _dbContext.Stations on a.DestinationStationId equals s.StationId into sa
                    from s in sa.DefaultIfEmpty()
                    select new DestinationAirportJoinResult {DestinationAirport = a, Station = s};

                joinQuery =
                    from a in joinQuery
                    join g in _dbContext.Geonames on a.Station.CityGeonameId equals g.GeonameId into ag
                    from g in ag.DefaultIfEmpty()
                    select new DestinationAirportJoinResult {DestinationAirport = a.DestinationAirport, Station = a.Station, City = g};

                return await joinQuery.ToListAsync();
            }

            return await query.Select(a => new DestinationAirportJoinResult
            {
                DestinationAirport = a
            }).ToListAsync();
        }
    }
}