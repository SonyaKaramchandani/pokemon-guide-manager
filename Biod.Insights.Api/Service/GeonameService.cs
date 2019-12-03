using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.Models;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Service
{
    public class GeonameService : IGeonameService
    {
        private readonly ILogger<UserLocationService> _logger;
        private readonly BiodZebraContext _biodZebraContext;

        /// <summary>
        /// Geoname service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        public GeonameService(BiodZebraContext biodZebraContext, ILogger<UserLocationService> logger)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
        }

        public async Task<GetGeonameModel> GetGeoname(int geonameId)
        {
            var geonames = await _biodZebraContext.Geonames
                .Where(g => g.GeonameId == geonameId)
                .ToListAsync();
            return geonames.Select(ConvertToModel).FirstOrDefault();
        }

        public async Task<IEnumerable<GetGeonameModel>> GetGeonames(IEnumerable<int> geonameIds)
        {
            var geonames = await _biodZebraContext.Geonames
                .Where(g => geonameIds.Contains(g.GeonameId))
                .ToListAsync();

            return geonames.Select(ConvertToModel).ToList();
        }

        private GetGeonameModel ConvertToModel(Geonames geoname)
        {//TODO: consider using automapper
            return new GetGeonameModel
            {
                GeonameId = geoname.GeonameId,
                LocationType = geoname.LocationType ?? -1,
                Name = geoname.Name,
                Country = geoname.CountryName
            };
        }
    }
}