using Biod.Insights.Api.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Exceptions;
using Biod.Insights.Api.Models.Geoname;

namespace Biod.Insights.Api.Service
{
    /// <summary>
    /// User Location service
    /// </summary>
    public class UserLocationService : IUserLocationService
    {
        private const int MAX_USER_LOCATION_AOI = 50; 
        
        private readonly ILogger<UserLocationService> _logger;
        private readonly BiodZebraContext _biodZebraContext;
        private readonly IGeonameService _geonameService;
        private readonly IRiskCalculationService _riskCalculationService;

        /// <summary>
        /// User Location service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        /// <param name="geonameService">The GeonameService</param>
        /// <param name="riskCalculationService">the risk calculation service</param>
        public UserLocationService(
            BiodZebraContext biodZebraContext,
            ILogger<UserLocationService> logger,
            IGeonameService geonameService,
            IRiskCalculationService riskCalculationService)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
            _geonameService = geonameService;
            _riskCalculationService = riskCalculationService;
        }

        public async Task<IEnumerable<GetGeonameModel>> GetAoi(string userId)
        {
            _logger.LogDebug($"GET AOIs for user called with userId {userId}");
            var aoiGeonameIds = await _biodZebraContext.AspNetUsers
                .Where(u => u.Id == userId)
                .Select(u => u.AoiGeonameIds)
                .FirstOrDefaultAsync();

            if (aoiGeonameIds == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested user with id {userId} does not exist");
            }

            var geonameIds = aoiGeonameIds.Split(',').Select(e => Convert.ToInt32(e)).ToList();

            return await _geonameService.GetGeonames(geonameIds);
        }

        public async Task<IEnumerable<GetGeonameModel>> AddAoi(string userId, int geonameId)
        {
            _logger.LogDebug($"Add AOIs for user called with userId {userId}; adding {geonameId}");
            var user = await _biodZebraContext.AspNetUsers
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, $"Requested user with id {userId} does not exist");
            }

            var currentGeonameIds = user.AoiGeonameIds.Split(',');
            if (currentGeonameIds.Length >= MAX_USER_LOCATION_AOI)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, $"User has reached maximum location limit of {MAX_USER_LOCATION_AOI}");
            }

            await _geonameService.GetGeoname(geonameId);

            var geonameIds = new HashSet<string>(currentGeonameIds) {geonameId.ToString()};
            user.AoiGeonameIds = string.Join(',', geonameIds);

            if (!_biodZebraContext.ActiveGeonames.Any(g => g.GeonameId == geonameId))
            {
                var result = _biodZebraContext.Database
                             .ExecuteSqlInterpolated($@"EXECUTE place.usp_InsertActiveGeonamesByGeonameIds
                                                     @GeonameIds = {geonameId}");
            }

            await _biodZebraContext.SaveChangesAsync();
            _logger.LogDebug($"Successfully added {geonameId} to AOIs for user {userId}");

            await _riskCalculationService.PreCalculateImportationRisk(geonameId);

            return await _geonameService.GetGeonames(geonameIds.Select(e => Convert.ToInt32(e)).ToList());
        }

        public async Task<IEnumerable<GetGeonameModel>> RemoveAoi(string userId, int geonameId)
        {
            _logger.LogDebug($"Remove AOI for user called with userId {userId}; removing {geonameId}");
            var user = await _biodZebraContext.AspNetUsers
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested user with id {userId} does not exist");
            }

            var geonameIds = new HashSet<string>(user.AoiGeonameIds.Split(','));
            geonameIds.Remove(geonameId.ToString());

            if (!geonameIds.Any())
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, $"Cannot remove the last geonameId for user");
            }

            user.AoiGeonameIds = string.Join(',', geonameIds);
            await _biodZebraContext.SaveChangesAsync();
            _logger.LogDebug($"Successfully added {geonameId} to AOIs for user {userId}");

            return await _geonameService.GetGeonames(geonameIds.Select(e => Convert.ToInt32(e)).ToList());
        }
    }
}