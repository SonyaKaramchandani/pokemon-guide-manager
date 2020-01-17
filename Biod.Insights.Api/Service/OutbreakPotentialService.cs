using System;
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

        public async Task<IEnumerable<OutbreakPotentialCategoryModel>> GetOutbreakPotentialByGeonameId(int geonameId)
        {
            var geoname = await _geonameService.GetGeoname(geonameId);
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
                    DiseaseId = p.Disease.DiseaseId,
                    Name = p.EffectiveMessage
                });
            }

            // Cache miss, call George and save
            return await ExecuteGeorgeApiCall(geoname);
        }

        public async Task<OutbreakPotentialCategoryModel> GetOutbreakPotentialByGeonameId(int diseaseId, int geonameId)
        {
            var geoname = await _geonameService.GetGeoname(geonameId);
            return await GetOutbreakPotentialByGeoname(diseaseId, geoname);
        }

        public async Task<OutbreakPotentialCategoryModel> GetOutbreakPotentialByGeoname(int diseaseId, GetGeonameModel geoname)
        {
            var disease = (await new DiseaseQueryBuilder(_biodZebraContext)
                    .AddDiseaseId(diseaseId)
                    .IncludeOutbreakPotentialCategories()
                    .BuildAndExecute())
                .First();

            var attributeId = disease.Disease.OutbreakPotentialAttributeId;

            if (attributeId != (int) Constants.OutbreakPotentialCategory.NeedsMapSustained
                && attributeId != (int) Constants.OutbreakPotentialCategory.NeedsMapUnlikely)
            {
                // Disease does not need map or cache
                var outbreakPotential = disease.OutbreakPotentialCategory.FirstOrDefault()
                                        ?? await _biodZebraContext.OutbreakPotentialCategory.FirstOrDefaultAsync(c => c.Id == (int) Constants.OutbreakPotentialCategory.Unknown);
                return ConvertOutbreakPotentialCategory(outbreakPotential, disease.Disease.DiseaseId);
            }

            var cachedOutbreakPotential = _biodZebraContext.GeonameOutbreakPotential.FirstOrDefault(p => p.GeonameId == geoname.GeonameId && p.DiseaseId == disease.Disease.DiseaseId);
            if (cachedOutbreakPotential != null)
            {
                // Disease needs map, and is found in the cache
                return new OutbreakPotentialCategoryModel
                {
                    Id = cachedOutbreakPotential.OutbreakPotentialId,
                    AttributeId = cachedOutbreakPotential.OutbreakPotentialAttributeId,
                    DiseaseId = disease.Disease.DiseaseId,
                    Name = cachedOutbreakPotential.EffectiveMessage
                };
            }

            // Cache miss, load from George and cache results
            return (await ExecuteGeorgeApiCall(geoname)).FirstOrDefault(p => p.DiseaseId == disease.Disease.DiseaseId)
                   ?? ConvertOutbreakPotentialCategory(
                       await _biodZebraContext.OutbreakPotentialCategory.FirstOrDefaultAsync(c => c.Id == (int) Constants.OutbreakPotentialCategory.Unknown),
                       disease.Disease.DiseaseId);
        }

        private async Task<IEnumerable<OutbreakPotentialCategoryModel>> ExecuteGeorgeApiCall([NotNull] GetGeonameModel geoname)
        {
            IEnumerable<OutbreakPotentialCategoryModel> results = new List<OutbreakPotentialCategoryModel>();

            try
            {
                if (geoname.LocationType == (int) Constants.LocationType.City)
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

            var defaultOutbreakPotential = await _biodZebraContext.OutbreakPotentialCategory.FirstOrDefaultAsync(c => c.Id == (int) Constants.OutbreakPotentialCategory.Unknown);

            return (await new DiseaseQueryBuilder(_biodZebraContext)
                    .IncludeOutbreakPotentialCategories()
                    .BuildAndExecute())
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
                        ? ConvertOutbreakPotentialCategory(outbreakPotential, d.Disease.DiseaseId)
                        : null;
                })
                .Where(d => d != null)
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