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
        private readonly IGeorgeApiService _georgeApiService;
        private readonly IGeonameService _geonameService;

        /// <summary>
        /// Risk service
        /// </summary>
        /// <param name="georgeApiService">The georgeApi service</param>
        /// <param name="logger">The logger</param>
        /// <param name="geonameService">The geoname service</param>
        public OutbreakPotentialService(
            IGeorgeApiService georgeApiService,
            ILogger<OutbreakPotentialService> logger,
            IGeonameService geonameService)
        {
            _georgeApiService = georgeApiService;
            _logger = logger;
            _geonameService = geonameService;
        }
        
        public async Task<IEnumerable<OutbreakPotentialCategoryModel>> GetOutbreakPotentialByPoint(float latitude, float longitude)
        {
            var risk = await _georgeApiService.GetLocationRisk(latitude,longitude);
            //map models here
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