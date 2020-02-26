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
    public class SourceAirportQueryBuilder : IQueryBuilder<EventSourceAirport, SourceAirportJoinResult>
    {
        [NotNull] private readonly BiodZebraContext _dbContext;

        private int? _eventId;

        private bool _includeAirportInformation;
        private bool _includeEventInformation;

        public SourceAirportQueryBuilder([NotNull] BiodZebraContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<EventSourceAirport> GetInitialQueryable()
        {
            return _dbContext.EventSourceAirport.AsQueryable();
        }

        public SourceAirportQueryBuilder SetEventId(int eventId)
        {
            _eventId = eventId;
            return this;
        }

        public SourceAirportQueryBuilder IncludeAll()
        {
            return this
                .IncludeAirportInformation()
                .IncludeEventInformation();
        }

        public SourceAirportQueryBuilder IncludeAirportInformation()
        {
            _includeAirportInformation = true;
            return this;
        }

        public SourceAirportQueryBuilder IncludeEventInformation()
        {
            _includeEventInformation = true;
            return this;
        }

        public async Task<IEnumerable<SourceAirportJoinResult>> BuildAndExecute()
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