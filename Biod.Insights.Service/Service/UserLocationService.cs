using Biod.Insights.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Models.Geoname;
using Biod.Insights.Common.Exceptions;
using Biod.Insights.Service.Configs;

namespace Biod.Insights.Service.Service
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

            var config = new GeonameConfig.Builder()
                .AddGeonameIds(aoiGeonameIds.Split(',').Select(e => Convert.ToInt32(e)).ToList())
                .Build();
            
            return await _geonameService.GetGeonames(config);
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

            await SetAois(user, new HashSet<int>(user.AoiGeonameIds.Split(',').Select(g => Convert.ToInt32(g))) {geonameId});
            var finalGeonameIds = new HashSet<string>(user.AoiGeonameIds.Split(','))
                .Select(e => Convert.ToInt32(e))
                .ToList();

            return await _geonameService.GetGeonames(new GeonameConfig.Builder().AddGeonameIds(finalGeonameIds).Build());
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

            var geonameIds = new HashSet<string>(user.AoiGeonameIds.Split(','))
                .Select(e => Convert.ToInt32(e))
                .ToList();
            geonameIds.Remove(geonameId);

            if (!geonameIds.Any())
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, $"Cannot remove the last geonameId for user");
            }

            user.AoiGeonameIds = string.Join(',', geonameIds);
            await _biodZebraContext.SaveChangesAsync();
            _logger.LogDebug($"Successfully added {geonameId} to AOIs for user {userId}");
            
            return await _geonameService.GetGeonames(new GeonameConfig.Builder().AddGeonameIds(geonameIds).Build());
        }

        public async Task<IEnumerable<GetGeonameModel>> SetAois(AspNetUsers user, ICollection<int> geonameIds)
        {
            if (geonameIds.Count >= MAX_USER_LOCATION_AOI)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, $"User has reached maximum location limit of {MAX_USER_LOCATION_AOI}");
            }

            var invalidGeonameIds = geonameIds.Except(_biodZebraContext.Geonames.Select(g => g.GeonameId).Where(g => geonameIds.Contains(g))).ToList();
            if (invalidGeonameIds.Any())
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest, $"The following geoname ids do not exist: {string.Join(',', invalidGeonameIds)}");
            }

            await UpdateUserAoi(user, geonameIds.Select(g => g.ToString()));
            _logger.LogDebug($"Successfully added {geonameIds} to AOIs for user {user.Id}");

            await _riskCalculationService.PreCalculateImportationRisk(geonameIds);
            
            return await _geonameService.GetGeonames(new GeonameConfig.Builder().AddGeonameIds(geonameIds).Build());
        }

        private async Task UpdateUserAoi(AspNetUsers user, IEnumerable<string> geonameIds)
        {
            user.AoiGeonameIds = string.Join(',', geonameIds);
            _biodZebraContext.Database.ExecuteSqlInterpolated($@"EXECUTE place.usp_InsertActiveGeonamesByGeonameIds
                                                                    @GeonameIds = {user.AoiGeonameIds}");

            await _biodZebraContext.SaveChangesAsync();
        }
    }
}