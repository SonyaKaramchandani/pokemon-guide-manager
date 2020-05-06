using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Data.EntityModels;
using Biod.Insights.Service.Data.QueryBuilders;
using Biod.Insights.Service.Interface;
using Biod.Insights.Service.Models;
using Biod.Insights.Service.Models.User;
using Biod.Products.Common.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Service.Service
{
    public class DiseaseRelevanceService : IDiseaseRelevanceService
    {
        private readonly ILogger<DiseaseRelevanceService> _logger;
        private readonly BiodZebraContext _biodZebraContext;

        /// <summary>
        /// Disease Relevance service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        public DiseaseRelevanceService(
            BiodZebraContext biodZebraContext,
            ILogger<DiseaseRelevanceService> logger)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
        }

        public async Task<DiseaseRelevanceSettingsModel> GetUserDiseaseRelevanceSettings(UserModel user)
        {
            // Get all available diseases
            var diseases = new HashSet<int>(new DiseaseQueryBuilder(_biodZebraContext).GetInitialQueryable().Select(d => d.DiseaseId));

            // Get the user configurations for diseases
            var diseaseRelevance = await _biodZebraContext.XtblUserDiseaseRelevance
                .Where(r => r.UserId == user.Id)
                .Select(r => new {r.DiseaseId, r.RelevanceId})
                .ToListAsync();

            // Create sets of disease ids
            var alwaysShown = new HashSet<int>(diseaseRelevance
                .Where(r => r.RelevanceId == (int) DiseaseRelevanceType.AlwaysNotify)
                .Select(dr => dr.DiseaseId));
            var riskOnly = new HashSet<int>(diseaseRelevance
                .Where(r => r.RelevanceId == (int) DiseaseRelevanceType.RiskOnly)
                .Select(dr => dr.DiseaseId));
            var neverShown = new HashSet<int>(diseaseRelevance
                .Where(r => r.RelevanceId == (int) DiseaseRelevanceType.NeverNotify)
                .Select(dr => dr.DiseaseId));

            // Keep only diseases that have not been configured
            diseases.ExceptWith(alwaysShown);
            diseases.ExceptWith(riskOnly);
            diseases.ExceptWith(neverShown);

            // Get the presets for the user type associated to the user
            var userRole = user.Roles.FirstOrDefault(r => r.IsPublic);
            if (userRole != null)
            {
                var userTypeSettings = await GetUserTypeDiseaseRelevanceSettings(Guid.Parse(userRole.Id));

                // Add disease ids that have not been used that are part of the default role
                alwaysShown.UnionWith(diseases.Intersect(userTypeSettings.AlwaysNotifyDiseaseIds));
                riskOnly.UnionWith(diseases.Intersect(userTypeSettings.RiskOnlyDiseaseIds));
                neverShown.UnionWith(diseases.Intersect(userTypeSettings.NeverNotifyDiseaseIds));

                // Keep only diseases that have not been configured
                diseases.ExceptWith(userTypeSettings.AlwaysNotifyDiseaseIds);
                diseases.ExceptWith(userTypeSettings.RiskOnlyDiseaseIds);
                diseases.ExceptWith(userTypeSettings.NeverNotifyDiseaseIds);
            }
            else
            {
                _logger.LogWarning($"User with id {user.Id} has no public roles: Unexpected behaviour may occur");
            }

            // Remaining diseases fall back to risk only (relevance type = 2)
            riskOnly.UnionWith(diseases);

            return new DiseaseRelevanceSettingsModel
            {
                AlwaysNotifyDiseaseIds = alwaysShown,
                RiskOnlyDiseaseIds = riskOnly,
                NeverNotifyDiseaseIds = neverShown
            };
        }

        public async Task<DiseaseRelevanceSettingsModel> GetUserTypeDiseaseRelevanceSettings(Guid userTypeId)
        {
            // Get all available diseases
            var diseases = new HashSet<int>(new DiseaseQueryBuilder(_biodZebraContext).GetInitialQueryable().Select(d => d.DiseaseId));

            var userTypeRelevance = await _biodZebraContext.UserTypeDiseaseRelevances
                .Where(rdr => rdr.UserTypeId == userTypeId)
                .Select(r => new {r.DiseaseId, r.RelevanceId})
                .ToListAsync();

            // Create sets for these disease ids
            var alwaysShown = new HashSet<int>(userTypeRelevance
                .Where(r => r.RelevanceId == (int) DiseaseRelevanceType.AlwaysNotify)
                .Select(dr => dr.DiseaseId));
            var riskOnly = new HashSet<int>(userTypeRelevance
                .Where(r => r.RelevanceId == (int) DiseaseRelevanceType.RiskOnly)
                .Select(dr => dr.DiseaseId));
            var neverShown = new HashSet<int>(userTypeRelevance
                .Where(r => r.RelevanceId == (int) DiseaseRelevanceType.NeverNotify)
                .Select(dr => dr.DiseaseId));

            // Add disease ids that have not been used that are part of the default user type
            alwaysShown.UnionWith(diseases.Intersect(alwaysShown));
            riskOnly.UnionWith(diseases.Intersect(riskOnly));
            neverShown.UnionWith(diseases.Intersect(neverShown));

            // Keep only diseases that have not been configured
            diseases.ExceptWith(alwaysShown);
            diseases.ExceptWith(riskOnly);
            diseases.ExceptWith(neverShown);

            // Remaining diseases fall back to risk only (relevance type = 2)
            riskOnly.UnionWith(diseases);

            return new DiseaseRelevanceSettingsModel
            {
                AlwaysNotifyDiseaseIds = alwaysShown,
                RiskOnlyDiseaseIds = riskOnly,
                NeverNotifyDiseaseIds = neverShown
            };
        }
    }
}