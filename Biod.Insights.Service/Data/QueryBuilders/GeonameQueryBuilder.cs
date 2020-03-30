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
    public class GeonameQueryBuilder : IQueryBuilder<Geonames, GeonameJoinResult>
    {
        [NotNull] private readonly BiodZebraContext _dbContext;
        [NotNull] private readonly GeonameConfig _config;

        private IQueryable<Geonames> _customInitialQueryable;

        public GeonameQueryBuilder([NotNull] BiodZebraContext dbContext) : this(dbContext, new GeonameConfig.Builder().Build())
        {
            
        }
        
        public GeonameQueryBuilder([NotNull] BiodZebraContext dbContext, GeonameConfig config)
        {
            _customInitialQueryable = null;
            _dbContext = dbContext;
            _config = config;
        }

        public IQueryable<Geonames> GetInitialQueryable()
        {
            return _customInitialQueryable ?? _dbContext.Geonames.AsQueryable().AsNoTracking();
        }

        public IQueryBuilder<Geonames, GeonameJoinResult> OverrideInitialQueryable(IQueryable<Geonames> customQueryable)
        {
            _customInitialQueryable = customQueryable;
            return this;
        }

        public async Task<IEnumerable<GeonameJoinResult>> BuildAndExecute()
        {
            var query = GetInitialQueryable();

            if (_config.GeonameIds.Any())
            {
                query = query.Where(g => _config.GeonameIds.Contains(g.GeonameId));
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
                    ShapeAsText = _config.IncludeShape ? g.CountryProvinceShapes.SimplifiedShapeText : null
                })
                .ToListAsync();
        }
    }
}