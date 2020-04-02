using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Biod.Insights.Service.Data.CustomModels;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Data.QueryBuilders;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.Geoname;
using Biod.Insights.Common.Constants;
using Biod.Insights.Common.Exceptions;
using Biod.Insights.Service.Configs;
using Biod.Insights.Service.Data;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Service.Service
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

        public async Task<GetGeonameModel> GetGeoname(GeonameConfig config)
        {
            var geoname = (await new GeonameQueryBuilder(_biodZebraContext, config).BuildAndExecute()).FirstOrDefault();

            if (geoname == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound, $"Requested geoname with id {config.GetGeonameId()} does not exist");
            }

            return ConvertToModel(geoname);
        }

        public async Task<IEnumerable<GetGeonameModel>> GetGeonames(GeonameConfig config)
        {
            var geonames = await new GeonameQueryBuilder(_biodZebraContext, config).BuildAndExecute();

            return geonames.Select(ConvertToModel).ToList();
        }

        public async Task<IEnumerable<SearchGeonameModel>> SearchGeonamesByTerm(string searchTerm)
        {
            var searchGeonames = await SqlQuery.SearchGeonames(_biodZebraContext, searchTerm);
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

        public async Task<IEnumerable<SearchGeonameModel>> SearchCitiesByTerm(string searchTerm)
        {
            var searchGeonames = await SqlQuery.SearchCityGeonames(_biodZebraContext, searchTerm);
            return searchGeonames
                .Where(item => item.DisplayName.IndexOf(searchTerm, StringComparison.InvariantCultureIgnoreCase) >= 0) // Existing behaviour, can be potentially removed
                .Select(g => new SearchGeonameModel
                {
                    GeonameId = g.GeonameId,
                    Name = g.DisplayName,
                    LocationType = LocationType.City,
                    Latitude = (float) g.Latitude,
                    Longitude = (float) g.Longitude
                }).ToList();
        }

        public IEnumerable<string> GetGridIdsByGeonameId(int geonameId)
        {
            return SqlQuery.GetGridIdsByGeoname(_biodZebraContext, geonameId, out _).AsEnumerable();
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