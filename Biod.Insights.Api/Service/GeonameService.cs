using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Biod.Insights.Api.Constants;
using Biod.Insights.Api.Data.CustomModels;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Data.QueryBuilders;
using Biod.Insights.Api.Exceptions;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models.Geoname;
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
                .IncludeProvince()
                .IncludeCountry()
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
                .IncludeProvince()
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

        private GetGeonameModel ConvertToModel(GeonameJoinResult geoname)
        {
            return new GetGeonameModel
            {
                GeonameId = geoname.Geoname.GeonameId,
                LocationType = geoname.Geoname.LocationType ?? -1,
                Name = geoname.Geoname.Name,
                Country = geoname.Geoname.CountryName,
                Province = geoname.Geoname.Admin1Geoname?.Name,
                Latitude = (float) (geoname.Geoname.Latitude ?? 0),
                Longitude = (float) (geoname.Geoname.Longitude ?? 0),
                Shape = geoname.Shape?.ToText()
            };
        }
    }
}