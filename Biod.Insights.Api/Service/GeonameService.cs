using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.CustomModels;
using Biod.Insights.Data.CustomModels;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Api.Data.QueryBuilders;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models.Geoname;
using Biod.Insights.Common.Constants;
using Biod.Insights.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Service
{
    public class GeonameService : IGeonameService
    {
        private readonly ILogger<GeonameService> _logger;
        private readonly BiodZebraContext _biodZebraContext;

        /// <summary>
        /// Geoname service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        public GeonameService(BiodZebraContext biodZebraContext, ILogger<GeonameService> logger)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
        }

        public async Task<GetGeonameModel> GetGeoname(int geonameId, bool includeShape = false)
        {
            var query = new GeonameQueryBuilder(_biodZebraContext)
                .AddGeonameId(geonameId);
            
            if (includeShape)
            {
                query = query.IncludeShape();
            }
            
            var geoname = (await query.BuildAndExecute()).FirstOrDefault();
            if (geoname == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested geoname with id {geonameId} does not exist");
            }

            return ConvertToModel(geoname);
        }

        public async Task<IEnumerable<GetGeonameModel>> GetGeonames(IEnumerable<int> geonameIds, bool includeShape = false)
        {
            var query = new GeonameQueryBuilder(_biodZebraContext)
                .AddGeonameIds(geonameIds);
            
            if (includeShape)
            {
                query = query.IncludeShape();
            }

            var geonames = await query.BuildAndExecute();

            return geonames.Select(ConvertToModel).ToList();
        }

        public async Task<IEnumerable<SearchGeonameModel>> SearchGeonamesByTerm(string searchTerm)
        {
            var searchGeonames = await _biodZebraContext.usp_SearchGeonames_Result
                .FromSqlInterpolated($"EXECUTE place.usp_SearchGeonames @inputTerm={searchTerm}")
                .ToListAsync();

            return searchGeonames.Select(g =>
            {
                Enum.TryParse<LocationType>(g.LocationType, out var locationType);
                return new SearchGeonameModel
                {
                    GeonameId = g.GeonameId,
                    Name = g.DisplayName,
                    LocationType = locationType,
                    Latitude = (float) g.Latitude,
                    Longitude = (float) g.Longitude
                };
            }).ToList();
        }

        public async Task<string> GetGridIdByGeonameId(int geonameId)
        {
            usp_ZebraPlaceGetGridIdByGeonameId_Result result = null;
            try
            {
                result = (await _biodZebraContext.usp_ZebraPlaceGetGridIdByGeonameId_Result
                        .FromSqlInterpolated($"EXECUTE zebra.usp_ZebraPlaceGetGridIdByGeonameId @GeonameId={geonameId}")
                        .ToListAsync())
                    .FirstOrDefault();
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Stored procedure 'zebra.usp_ZebraPlaceGetGridIdByGeonameId' failed with geoname id {geonameId}", e);
            }

            if (string.IsNullOrWhiteSpace(result?.GridId))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested geoname with id {geonameId} does not exist");
            }

            return result.GridId;
        }

        private GetGeonameModel ConvertToModel(GeonameJoinResult geoname)
        {
            return new GetGeonameModel
            {
                GeonameId = geoname.Id,
                LocationType = geoname.LocationType,
                Name = geoname.Name,
                FullDisplayName = geoname.DisplayName,
                Country = geoname.CountryName,
                Province = geoname.ProvinceName,
                Latitude = geoname.Latitude,
                Longitude = geoname.Longitude,
                Shape = geoname.ShapeAsText
            };
        }
    }
}