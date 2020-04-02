using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Common.Constants;
using Biod.Insights.Data.CustomModels;
using Biod.Insights.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Biod.Insights.Service.Data
{
    public static class SqlQuery
    {
        public static void AddActiveGeonames(BiodZebraContext dbContext, string geonameIds)
        {
            dbContext.Database.ExecuteSqlInterpolated($@"EXECUTE place.usp_InsertActiveGeonamesByGeonameIds
                                                            @GeonameIds = {geonameIds}");
        }

        public static async Task<List<usp_ZebraDiseaseGetLocalCaseCount_Result>> GetLocalCaseCount(
            BiodZebraContext dbContext,
            int diseaseId,
            int? geonameId,
            int? eventId)
        {
            return await dbContext.usp_ZebraDiseaseGetLocalCaseCount_Result
                .FromSqlInterpolated($@"EXECUTE zebra.usp_ZebraDiseaseGetLocalCaseCount
                                            @DiseaseId = {diseaseId},
                                            @GeonameIds = {(geonameId.HasValue ? geonameId.Value.ToString() : "")},
                                            @EventId = {eventId}")
                .ToListAsync();
        }

        public static async Task<usp_ZebraDataRenderSetImportationRiskByGeonameId_Result.StoredProcedureReturnCode> ExecuteImportationRiskCalculation(BiodZebraContext dbContext, int geonameId)
        {
            return (usp_ZebraDataRenderSetImportationRiskByGeonameId_Result.StoredProcedureReturnCode) (await dbContext.usp_ZebraDataRenderSetImportationRiskByGeonameId_Result
                    .FromSqlInterpolated($@"EXECUTE zebra.usp_ZebraDataRenderSetImportationRiskByGeonameIdSpreadMd @GeonameId = {geonameId}")
                    .ToListAsync())
                .First().Result;
        }

        public static async Task<bool> UpdateEventLocationHistory(BiodZebraContext dbContext, int eventId)
        {
            return (await dbContext.usp_ZebraEventSetEventCase_Result
                    .FromSqlInterpolated($@"EXECUTE zebra.usp_ZebraEventSetEventCase @EventId = {eventId}")
                    .ToListAsync())
                .First()
                .Result;
        }

        public static async Task<List<usp_SearchGeonames_Result>> SearchGeonames(BiodZebraContext dbContext, string searchTerm)
        {
            return await dbContext.usp_SearchGeonames_Result
                .FromSqlInterpolated($"EXECUTE place.usp_SearchGeonames @inputTerm={searchTerm}")
                .ToListAsync();
        }

        public static async Task<List<usp_GetGeonameCities_Result>> SearchCityGeonames(BiodZebraContext dbContext, string searchTerm)
        {
            return await dbContext.usp_GetGeonameCities_Result
                .FromSqlInterpolated($"EXECUTE place.usp_GetGeonameCities @inputTerm={searchTerm}")
                .ToListAsync();
        }

        public static async Task<List<ufn_ZebraGetLocalUserLocationsByGeonameId_Result>> GetUsersWithinGeoname(BiodZebraContext dbContext, int geonameId, int diseaseId)
        {
            return await dbContext.ufn_ZebraGetLocalUserLocationsByGeonameId_Result
                .FromSqlInterpolated($@"SELECT DISTINCT UserId, UserGeonameId 
                                        FROM bd.ufn_ZebraGetLocalUserLocationsByGeonameId({geonameId}, 0, 0, 0, {diseaseId})")
                .ToListAsync();
        }

        /// <summary>
        /// Get the grid ids of a given geoname id
        ///
        /// For geographically large geonames (e.g. Canada), enumerating the queryable will load all the Grids into memory, which may slow down performance if
        /// part of a query.
        /// </summary>
        /// <param name="dbContext">the db context</param>
        /// <param name="geonameId">the geoname id</param>
        /// <param name="isEnumerated">whether the results have already been enumerated</param>
        /// <returns></returns>
        public static IQueryable<string> GetGridIdsByGeoname(BiodZebraContext dbContext, int geonameId, out bool isEnumerated)
        {
            var geoname = dbContext.Geonames
                .Select(g => new {g.GeonameId, g.LocationType})
                .FirstOrDefault(g => g.GeonameId == geonameId);

            if (geoname == null)
            {
                isEnumerated = true;
                return new List<string>().AsQueryable();
            }

            switch ((LocationType) (geoname.LocationType ?? 0))
            {
                case LocationType.Country:
                    isEnumerated = false;
                    return dbContext.GridCountry.Where(g => g.CountryGeonameId == geonameId).Select(g => g.GridId);
                case LocationType.Province:
                    isEnumerated = false;
                    return dbContext.GridProvince.Where(g => g.Adm1GeonameId == geonameId).Select(g => g.GridId);
                default:
                    isEnumerated = true;
                    return dbContext.usp_ZebraPlaceGetGridIdByGeonameId_Result.FromSqlInterpolated($"EXECUTE zebra.usp_ZebraPlaceGetGridIdByGeonameId @GeonameId={geonameId}")
                        .Select(r => r.GridId)
                        .AsEnumerable()
                        .AsQueryable();
            }
        }

        public static List<usp_ZebraEventGetArticlesByEventId_Result> GetArticlesByEvent(BiodZebraContext dbContext, int eventId)
        {
            return dbContext.usp_ZebraEventGetArticlesByEventId_Result
                .FromSqlInterpolated($@"EXECUTE zebra.usp_ZebraEventGetArticlesByEventId @EventId = {eventId}")
                .ToList();
        }
    }
}