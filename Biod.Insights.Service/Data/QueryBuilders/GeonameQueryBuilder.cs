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
    public class GeonameQueryBuilder : IQueryBuilder<Geonames, GeonameJoinResult>
    {
        [NotNull] private readonly BiodZebraContext _dbContext;

        private IQueryable<Geonames> _customInitialQueryable;
        private readonly HashSet<int> _geonameIds = new HashSet<int>();

        private bool _includeShape;

        public GeonameQueryBuilder([NotNull] BiodZebraContext dbContext)
        {
            _customInitialQueryable = null;
            _dbContext = dbContext;
        }

        public IQueryable<Geonames> GetInitialQueryable()
        {
            return _customInitialQueryable ?? _dbContext.Geonames.AsQueryable();
        }

        public IQueryBuilder<Geonames, GeonameJoinResult> OverrideInitialQueryable(IQueryable<Geonames> customQueryable)
        {
            _customInitialQueryable = customQueryable;
            return this;
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
            var query = GetInitialQueryable();

            if (_geonameIds.Any())
            {
                query = query.Where(g => _geonameIds.Contains(g.GeonameId));
            }

            return await query
                .Select(g => new GeonameJoinResult
                {
                    Id = g.GeonameId,
                    Name = g.Name,
                    DisplayName = g.DisplayName,
                    LocationType = g.LocationType ?? -1,
                    CountryName = g.CountryName,
                    ProvinceName = g.Admin1Geoname.Name,
                    Latitude = (float) (g.Latitude ?? 0),
                    Longitude = (float) (g.Longitude ?? 0),
                    ShapeAsText = _includeShape ? g.CountryProvinceShapes.SimplifiedShapeText : null
                })
                .ToListAsync();
        }
    }
}