using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biod.Insights.Api.Data.EntityModels;
using Biod.Insights.Api.Data.QueryBuilders;
using Biod.Insights.Api.Interface;
using Biod.Insights.Api.Models;
using Biod.Insights.Api.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Biod.Insights.Api.Service
{
    public class DiseaseRelevanceService : IDiseaseRelevanceService
    {
        private readonly ILogger<DiseaseRelevanceService> _logger;
        private readonly BiodZebraContext _biodZebraContext;
        private readonly IUserService _userService;

        /// <summary>
        /// Disease Relevance service
        /// </summary>
        /// <param name="biodZebraContext">The db context</param>
        /// <param name="logger">The logger</param>
        /// <param name="userService">the user service</param>
        public DiseaseRelevanceService(
            BiodZebraContext biodZebraContext,
            ILogger<DiseaseRelevanceService> logger,
            IUserService userService)
        {
            _biodZebraContext = biodZebraContext;
            _logger = logger;
            _userService = userService;
        }

        public async Task<DiseaseRelevanceSettingsModel> GetUserDiseaseRelevanceSettings(string userId)
        {
            var user = await _userService.GetUser(userId);
            
            // Get all available diseases
            var diseases = new HashSet<int>(new DiseaseQueryBuilder(_biodZebraContext).GetInitialQueryable().Select(d => d.DiseaseId));
            
            // Get the user configurations for diseases
            var diseaseRelevance = await _biodZebraContext.XtblUserDiseaseRelevance
                .Where(r => r.UserId == user.Id)
                .Select(r => new {r.DiseaseId, r.RelevanceId})
                .ToListAsync();
            
            // Create sets of disease ids
            var alwaysShown = new HashSet<int>(diseaseRelevance
                .Where(r => r.RelevanceId == (int) Constants.DiseaseRelevanceType.AlwaysNotify)
                .Select(dr => dr.DiseaseId));
            var riskOnly = new HashSet<int>(diseaseRelevance
                .Where(r => r.RelevanceId == (int) Constants.DiseaseRelevanceType.RiskOnly)
                .Select(dr => dr.DiseaseId));
            var neverShown = new HashSet<int>(diseaseRelevance
                .Where(r => r.RelevanceId == (int) Constants.DiseaseRelevanceType.NeverNotify)
                .Select(dr => dr.DiseaseId));
            
            // Keep only diseases that have not been configured
            diseases.ExceptWith(alwaysShown);
            diseases.ExceptWith(riskOnly);
            diseases.ExceptWith(neverShown);
            
            // Get the role presets for the role associated to the user
            var userRole = user.Roles.First(r => r.IsPublic);
            var defaultRoleRelevance = _biodZebraContext.XtblRoleDiseaseRelevance
                .Where(rdr => rdr.RoleId == userRole.Id)
                .Select(r => new {r.DiseaseId, r.RelevanceId})
                .ToList();
            
            // Create sets for these disease ids
            var roleAlwaysShown = new HashSet<int>(defaultRoleRelevance
                .Where(r => r.RelevanceId == (int) Constants.DiseaseRelevanceType.AlwaysNotify)
                .Select(dr => dr.DiseaseId));
            var roleRiskOnly = new HashSet<int>(defaultRoleRelevance
                .Where(r => r.RelevanceId == (int) Constants.DiseaseRelevanceType.RiskOnly)
                .Select(dr => dr.DiseaseId));
            var roleNeverShown = new HashSet<int>(defaultRoleRelevance
                .Where(r => r.RelevanceId == (int) Constants.DiseaseRelevanceType.NeverNotify)
                .Select(dr => dr.DiseaseId));
            
            // Add disease ids that have not been used that are part of the default role
            alwaysShown.UnionWith(diseases.Intersect(roleAlwaysShown));
            riskOnly.UnionWith(diseases.Intersect(roleRiskOnly));
            neverShown.UnionWith(diseases.Intersect(roleNeverShown));
            
            // Keep only diseases that have not been configured
            diseases.ExceptWith(roleAlwaysShown);
            diseases.ExceptWith(roleRiskOnly);
            diseases.ExceptWith(roleNeverShown);
            
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