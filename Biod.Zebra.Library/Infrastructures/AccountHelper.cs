using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Models;
using Biod.Zebra.Library.Models.DiseaseRelevance;

namespace Biod.Zebra.Library.Infrastructures
{
    public static class AccountHelper
    {
        public static IEnumerable<int> GetRelevantDiseaseIds(BiodZebraEntities dbContext, ApplicationUser user)
        {
            // Get all available diseases
            var diseases = dbContext.usp_ZebraDashboardGetDiseases();
            
            // Get any customizations made by the user
            var userDiseaseRelevance = dbContext.Xtbl_User_Disease_Relevance
                .Where(udr => udr.UserId == user.Id)
                .ToDictionary(udr => udr.DiseaseId, udr => udr.RelevanceId);

            // Get the role presets for the role associated to the user
            var userRole = new CustomRolesFilter(dbContext).GetFirstPublicRole(user.Roles);
            var defaultRoleRelevance = dbContext.Xtbl_Role_Disease_Relevance
                .Where(rdr => rdr.RoleId == userRole.RoleId)
                .ToDictionary(rdr => rdr.DiseaseId, rdr => rdr.RelevanceId);

            var diseaseList = diseases
                .Where(d =>
                    // user's custom notification setting for that disease is not turned off
                    userDiseaseRelevance.ContainsKey(d.DiseaseId) && userDiseaseRelevance[d.DiseaseId] != 3
                    
                    // no customization for that disease, fallback to the role preset for that disease if it exists
                    || !userDiseaseRelevance.ContainsKey(d.DiseaseId) && defaultRoleRelevance.ContainsKey(d.DiseaseId) && defaultRoleRelevance[d.DiseaseId] != 3
                    
                    // no customization or role preset for that disease, default to notify
                    || !userDiseaseRelevance.ContainsKey(d.DiseaseId) && !defaultRoleRelevance.ContainsKey(d.DiseaseId)
                )
                .Select(d => d.DiseaseId)
                .ToList();

            if (!diseaseList.Any())
            {
                diseaseList.Add(-1);
            }

            return diseaseList;
        }

        public static UserRelevanceSettingsModel GetUserRelevanceSettings(BiodZebraEntities dbContext, ApplicationUser user)
        {
            // Get all available diseases
            var diseases = new HashSet<int>(dbContext.usp_ZebraDashboardGetDiseases().Select(d => d.DiseaseId));
            
            // Get the user configurations for diseases
            var diseaseRelevance = dbContext.Xtbl_User_Disease_Relevance
                .Where(r => r.UserId == user.Id)
                .Select(r => new {r.DiseaseId, r.RelevanceId})
                .ToList();
            
            // Create sets of disease ids
            var alwaysShown = new HashSet<int>(diseaseRelevance.Where(r => r.RelevanceId == Constants.RelevanceTypes.ALWAYS_NOTIFY).Select(dr => dr.DiseaseId));
            var riskOnly = new HashSet<int>(diseaseRelevance.Where(r => r.RelevanceId == Constants.RelevanceTypes.RISK_ONLY).Select(dr => dr.DiseaseId));
            var neverShown = new HashSet<int>(diseaseRelevance.Where(r => r.RelevanceId == Constants.RelevanceTypes.NEVER_NOTIFY).Select(dr => dr.DiseaseId));
            
            // Keep only diseases that have not been configured
            diseases.ExceptWith(alwaysShown);
            diseases.ExceptWith(riskOnly);
            diseases.ExceptWith(neverShown);
            
            // Get the role presets for the role associated to the user
            var userRole = new CustomRolesFilter(dbContext).GetFirstPublicRole(user.Roles);
            var defaultRoleRelevance = dbContext.Xtbl_Role_Disease_Relevance
                .Where(rdr => rdr.RoleId == userRole.RoleId)
                .Select(r => new {r.DiseaseId, r.RelevanceId})
                .ToList();
            
            // Create sets for these disease ids
            var roleAlwaysShown = new HashSet<int>(defaultRoleRelevance.Where(r => r.RelevanceId == Constants.RelevanceTypes.ALWAYS_NOTIFY).Select(dr => dr.DiseaseId));
            var roleRiskOnly = new HashSet<int>(defaultRoleRelevance.Where(r => r.RelevanceId == Constants.RelevanceTypes.RISK_ONLY).Select(dr => dr.DiseaseId));
            var roleNeverShown = new HashSet<int>(defaultRoleRelevance.Where(r => r.RelevanceId == Constants.RelevanceTypes.NEVER_NOTIFY).Select(dr => dr.DiseaseId));
            
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

            return new UserRelevanceSettingsModel
            {
                AlwaysNotifyDiseaseIds = alwaysShown,
                RiskOnlyDiseaseIds = riskOnly,
                NeverNotifyDiseaseIds = neverShown
            };
        }
        
        public static void PrecalculateRisk(string userId)
        {
            BiodZebraEntities zebraDbContext = new BiodZebraEntities();
            zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));
            // Do not wait for SP to finish, it takes too long. The callers do not need SP results anyway.
            // The results (EventImportationRisksByUser table) are used for sending weekly emails only.
            Task.Run(() => zebraDbContext.usp_ZebraDataRenderSetImportationRiskByUserId(userId));
        }

        public static void PrecalculateRiskByEvent(BiodZebraEntities zebraDbContext, int eventId)
        {
            zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));
            zebraDbContext.usp_ZebraDataRenderSetImportationRiskByEventId(eventId);
            zebraDbContext.usp_ZebraDataRenderSetGeonameImportationRiskByEventId(eventId);
        }
    }
}