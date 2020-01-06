using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Data.QueryBuilders;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models.Disease;
using Biod.Insights.Api.Models.Geoname;
using Biod.Insights.Api.Models.Risk;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Service
{
    public class OutbreakPotentialService : IOutbreakPotentialService
    {
        private readonly ILogger<OutbreakPotentialService> _logger;
        private readonly BiodZebraContext _biodZebraContext;
        private readonly IGeorgeApiService _georgeApiService;
        private readonly IGeonameService _geonameService;

        /// <summary>
        /// Risk service
        /// </summary>
        /// <param name="georgeApiService">The georgeApi service</param>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        /// <param name="geonameService">The geoname service</param>
        public OutbreakPotentialService(
            IGeorgeApiService georgeApiService,
            BiodZebraContext biodZebraContext,
            ILogger<OutbreakPotentialService> logger,
            IGeonameService geonameService)
        {
            _georgeApiService = georgeApiService;
            _biodZebraContext = biodZebraContext;
            _logger = logger;
            _geonameService = geonameService;
        }

        public async Task<IEnumerable<OutbreakPotentialCategoryModel>> GetOutbreakPotentialByPoint(float latitude, float longitude)
        {
            try
            {
                var risk = await _georgeApiService.GetLocationRisk(latitude, longitude);
                return await ConvertToModel(risk);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Failed to retrieve location risks from George API.");
            }

            return new List<OutbreakPotentialCategoryModel>();
        }

        public async Task<IEnumerable<OutbreakPotentialCategoryModel>> GetOutbreakPotentialByGeonameId(int geonameId)
        {
            var geoname = await _geonameService.GetGeoname(geonameId);
            return await GetOutbreakPotentialByGeoname(geoname);
        }

        public async Task<IEnumerable<OutbreakPotentialCategoryModel>> GetOutbreakPotentialByGeoname([NotNull] GetGeonameModel geoname)
        {
            if (geoname.LocationType == (int) Constants.LocationType.City)
            {
                return await GetOutbreakPotentialByPoint(geoname.Latitude, geoname.Longitude);
            }

            try
            {
                var risk = await _georgeApiService.GetLocationRisk(geoname.GeonameId);
                return await ConvertToModel(risk);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Failed to retrieve location risks from George API.");
            }

            return new List<OutbreakPotentialCategoryModel>();
        }

        private async Task<IEnumerable<OutbreakPotentialCategoryModel>> ConvertToModel(GeorgeRiskClass georgeRiskModel)
        {
            var georgeDiseases = georgeRiskModel
                .locations.First().diseaseRisks
                .ToDictionary(r => r.diseaseId, r => r.defaultRisk.riskValue);

            var defaultOutbreakPotential = await _biodZebraContext.OutbreakPotentialCategory.FirstOrDefaultAsync(c => c.Id == (int) Constants.OutbreakPotentialCategory.Unknown);

            return (await new DiseaseQueryBuilder(_biodZebraContext)
                    .IncludeOutbreakPotentialCategories()
                    .BuildAnExecute())
                .Select(d =>
                {
                    var outbreakPotential = defaultOutbreakPotential;
                    if (d.OutbreakPotentialCategory.Any(o => o.NeedsMap)
                        && georgeDiseases.ContainsKey(d.Disease.DiseaseId) && georgeDiseases[d.Disease.DiseaseId] > 0)
                    {
                        outbreakPotential = d.OutbreakPotentialCategory.FirstOrDefault(o => o.MapThreshold == ">0");
                    }
                    else if (d.OutbreakPotentialCategory.Any())
                    {
                        outbreakPotential = d.OutbreakPotentialCategory.First();
                    }

                    return outbreakPotential != null
                        ? new OutbreakPotentialCategoryModel
                        {
                            Id = outbreakPotential.AttributeId,
                            DiseaseId = d.Disease.DiseaseId,
                            Name = outbreakPotential.EffectiveMessage
                        }
                        : null;
                })
                .Where(d => d != null)
                .AsEnumerable();
        }
    }
}