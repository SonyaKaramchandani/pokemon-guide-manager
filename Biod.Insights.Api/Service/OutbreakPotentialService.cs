using System.Collections.Generic;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models.Disease;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Service
{
    public class OutbreakPotentialService : IOutbreakPotentialService
    {
        private readonly ILogger<OutbreakPotentialService> _logger;
        private readonly BiodZebraContext _biodZebraContext;
        private readonly IGeonameService _geonameService;

        /// <summary>
        /// Risk service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        /// <param name="geonameService">The geoname service</param>
        public OutbreakPotentialService(
            BiodZebraContext biodZebraContext,
            ILogger<OutbreakPotentialService> logger,
            IGeonameService geonameService)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
            _geonameService = geonameService;
        }
        
        public async Task<IEnumerable<OutbreakPotentialCategoryModel>> GetOutbreakPotentialByPoint(float latitude, float longitude)
        {
            return new List<OutbreakPotentialCategoryModel>();
        }

        public async Task<IEnumerable<OutbreakPotentialCategoryModel>> GetOutbreakPotentialByGeonameId(int geonameId)
        {
            var geoname = await _geonameService.GetGeoname(geonameId);
            if (geoname.LocationType == (int) Constants.LocationType.City)
            {
                return await GetOutbreakPotentialByPoint(geoname.Latitude, geoname.Longitude);
            }
            
            return new List<OutbreakPotentialCategoryModel>();
        }
    }
}