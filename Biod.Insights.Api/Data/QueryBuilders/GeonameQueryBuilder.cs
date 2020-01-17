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
    public class GeonameQueryBuilder : IQueryBuilder<Geonames, GeonameJoinResult>
    {
        [NotNull] private readonly BiodZebraContext _dbContext;

        private readonly HashSet<int> _geonameIds = new HashSet<int>();

        private bool _includeShape;
        private bool _includeProvince;
        private bool _includeCountry;

        public GeonameQueryBuilder([NotNull] BiodZebraContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Geonames> GetInitialQueryable()
        {
            return _dbContext.Geonames.AsQueryable();
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
        
        public GeonameQueryBuilder IncludeProvince()
        {
            _includeProvince = true;
            return this;
        }

        public GeonameQueryBuilder IncludeCountry()
        {
            _includeCountry = true;
            return this;
        }

        public async Task<IEnumerable<GeonameJoinResult>> BuildAndExecute()
        {
            var query = GetInitialQueryable();

            if (_geonameIds.Any())
            {
                query = query.Where(g => _geonameIds.Contains(g.GeonameId));
            }
            
            if (_includeProvince)
            {
                query = query.Include(g => g.Admin1Geoname);
            }
            
            if (_includeCountry)
            {
                query = query.Include(g => g.CountryGeoname);
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