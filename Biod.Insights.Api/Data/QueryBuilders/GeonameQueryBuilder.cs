using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.CustomModels;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Interface;
using Microsoft.EntityFrameworkCore;

namespace Biod.Insights.Api.Data.QueryBuilders
{
    public class GeonameQueryBuilder : IQueryBuilder<GeonameJoinResult>
    {
        [NotNull] private readonly BiodZebraContext _dbContext;

        private HashSet<int> _geonameIds = new HashSet<int>();

        private bool _includeShape;

        public GeonameQueryBuilder([NotNull] BiodZebraContext dbContext)
        {
            _dbContext = dbContext;
        }

        public GeonameQueryBuilder AddGeonameId(int geonameId)
        {
            _geonameIds.Add(geonameId);
            return this;
        }

        public GeonameQueryBuilder AddGeonameIds(IEnumerable<int> geonameIds)
        {
            _geonameIds.UnionWith(geonameIds);
            return this;
        }

        public GeonameQueryBuilder IncludeShape()
        {
            _includeShape = true;
            return this;
        }

        public async Task<IEnumerable<GeonameJoinResult>> BuildAndExecute()
        {
            var query = _dbContext.Geonames.AsQueryable();

            if (_geonameIds.Any())
            {
                query = query.Where(g => _geonameIds.Contains(g.GeonameId));
            }

            // Queries involving join
            var joinQuery = query.Select(g => new GeonameJoinResult {Geoname = g});
            if (_includeShape)
            {
                joinQuery =
                    from g in joinQuery
                    join s in _dbContext.CountryProvinceShapes on g.Geoname.GeonameId equals s.GeonameId into gs
                    from s in gs.DefaultIfEmpty()
                    select new GeonameJoinResult {Geoname = g.Geoname, Shape = s.SimplifiedShape};
            }

            return await joinQuery.ToListAsync();
        }
    }
}