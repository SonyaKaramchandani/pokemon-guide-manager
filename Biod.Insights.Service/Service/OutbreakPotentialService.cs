using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Data.QueryBuilders;
using Biod.Insights.Service.Helpers;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models.Disease;
using Biod.Insights.Service.Models.Geoname;
using Biod.Insights.Service.Models.Risk;
using Biod.Insights.Common.Constants;
using Biod.Insights.Service.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OutbreakPotentialCategory = Biod.Insights.Data.EntityModels.OutbreakPotentialCategory;

namespace Biod.Insights.Service.Service
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

        public async Task<IEnumerable<OutbreakPotentialCategoryModel>> GetOutbreakPotentialByGeonameId(int geonameId)
        {
            var geoname = await _geonameService.GetGeoname(new GeonameConfig.Builder().AddGeonameId(geonameId).Build());
            return await GetOutbreakPotentialByGeoname(geoname);
        }

        public async Task<IEnumerable<OutbreakPotentialCategoryModel>> GetOutbreakPotentialByGeoname([NotNull] GetGeonameModel geoname)
        {
            // Check if results are in cache
            var cachedOutbreakPotentials = await _biodZebraContext.GeonameOutbreakPotential
                .Include(p => p.Disease)
                .Where(p => p.GeonameId == geoname.GeonameId)
                .ToListAsync();
            if (cachedOutbreakPotentials.Any())
            {
                return cachedOutbreakPotentials.Select(p => new OutbreakPotentialCategoryModel
                {
                    Id = p.OutbreakPotentialId,
                    AttributeId = p.OutbreakPotentialAttributeId,
                    DiseaseId = p.DiseaseId,
                    Name = p.EffectiveMessage
                });
            }

            // Cache miss, call George and save
            return await ExecuteGeorgeApiCall(geoname);
        }

        public async Task<OutbreakPotentialCategoryModel> GetOutbreakPotentialByGeonameId(int diseaseId, int geonameId)
        {
            var geoname = await _geonameService.GetGeoname(new GeonameConfig.Builder().AddGeonameId(geonameId).Build());
            return await GetOutbreakPotentialByGeoname(diseaseId, geoname);
        }

        public async Task<OutbreakPotentialCategoryModel> GetOutbreakPotentialByGeoname(int diseaseId, GetGeonameModel geoname)
        {
            var disease = (await new DiseaseQueryBuilder(_biodZebraContext, new DiseaseConfig.Builder()
                        .AddDiseaseId(diseaseId)
                        .Build())
                    .BuildAndExecute())
                .First();

            if (!OutbreakPotentialCategoryHelper.IsMapNeeded(disease.OutbreakPotentialAttributeId))
            {
                // Disease does not need map or cache
                var outbreakPotential = OutbreakPotentialCategoryHelper.GetOutbreakPotentialCategory(disease.OutbreakPotentialAttributeId);
                outbreakPotential.DiseaseId = disease.DiseaseId;
                return outbreakPotential;
            }

            var cachedOutbreakPotential = _biodZebraContext.GeonameOutbreakPotential.FirstOrDefault(p => p.GeonameId == geoname.GeonameId && p.DiseaseId == disease.DiseaseId);
            if (cachedOutbreakPotential != null)
            {
                // Disease needs map, and is found in the cache
                return new OutbreakPotentialCategoryModel
                {
                    Id = cachedOutbreakPotential.OutbreakPotentialId,
                    AttributeId = cachedOutbreakPotential.OutbreakPotentialAttributeId,
                    DiseaseId = disease.DiseaseId,
                    Name = cachedOutbreakPotential.EffectiveMessage
                };
            }

            // Cache miss, load from George and cache results
            return (await ExecuteGeorgeApiCall(geoname)).FirstOrDefault(p => p.DiseaseId == disease.DiseaseId)
                   ?? ConvertOutbreakPotentialCategory(
                       await _biodZebraContext.OutbreakPotentialCategory.FirstOrDefaultAsync(c => c.Id == (int) Biod.Insights.Common.Constants.OutbreakPotentialCategory.Unknown),
                       disease.DiseaseId);
        }

        private async Task<IEnumerable<OutbreakPotentialCategoryModel>> ExecuteGeorgeApiCall([NotNull] GetGeonameModel geoname)
        {
            IEnumerable<OutbreakPotentialCategoryModel> results = new List<OutbreakPotentialCategoryModel>();

            try
            {
                if (geoname.LocationType == (int) LocationType.City)
                {
                    var risk = await _georgeApiService.GetLocationRisk(geoname.Latitude, geoname.Longitude);
                    results = await ConvertResponseToModel(risk);
                }
                else
                {
                    var risk = await _georgeApiService.GetLocationRisk(geoname.GeonameId);
                    results = await ConvertResponseToModel(risk);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Failed to retrieve location risks from George API.");
            }

            // Save the results into the cache
            try
            {
                _biodZebraContext.GeonameOutbreakPotential.AddRange(results.Select(o => new GeonameOutbreakPotential
                {
                    DiseaseId = o.DiseaseId,
                    GeonameId = geoname.GeonameId,
                    OutbreakPotentialId = o.Id,
                    OutbreakPotentialAttributeId = o.AttributeId,
                    EffectiveMessage = o.Name
                }));
                await _biodZebraContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save results to database from George API");
            }

            return results;
        }

        private async Task<IEnumerable<OutbreakPotentialCategoryModel>> ConvertResponseToModel(GeorgeRiskClass georgeRiskModel)
        {
            var georgeDiseases = georgeRiskModel
                .locations.First().diseaseRisks
                .ToDictionary(r => r.diseaseId, r => r.defaultRisk.riskValue);

            return (await new DiseaseQueryBuilder(_biodZebraContext).BuildAndExecute())
                .Select(d =>
                {
                    var outbreakPotential = OutbreakPotentialCategoryHelper.GetOutbreakPotentialCategory(
                        d.OutbreakPotentialAttributeId,
                        OutbreakPotentialCategoryHelper.IsMapNeeded(d.OutbreakPotentialAttributeId)
                        && georgeDiseases.ContainsKey(d.DiseaseId)
                        && georgeDiseases[d.DiseaseId] > 0);
                    outbreakPotential.DiseaseId = d.DiseaseId;
                    return outbreakPotential;
                })
                .AsEnumerable();
        }

        private OutbreakPotentialCategoryModel ConvertOutbreakPotentialCategory(OutbreakPotentialCategory outbreakPotential, int diseaseId)
        {
            return new OutbreakPotentialCategoryModel
            {
                Id = outbreakPotential.Id,
                AttributeId = outbreakPotential.AttributeId,
                DiseaseId = diseaseId,
                Name = outbreakPotential.EffectiveMessage
            };
        }
    }
}