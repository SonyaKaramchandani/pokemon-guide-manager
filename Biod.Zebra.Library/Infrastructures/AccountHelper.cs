using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using Biod.Zebra.Library.EntityModels;
using Biod.Zebra.Library.Infrastructures;
using Biod.Zebra.Library.Models;

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
        
        public static async Task<bool> PrecalculateRisk(string userId)
        {
            var retVal = false;

            try
            {

                BiodZebraEntities zebraDbContext = new BiodZebraEntities();
                zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));
                var t = Task.Run(() => zebraDbContext.usp_ZebraDataRenderSetImportationRiskByUserId(userId));
                await t.ContinueWith(x =>
                {
                    if (x.Result.FirstOrDefault() == 1) { retVal = true; }
                });
            }
            catch (Exception)
            {
                retVal = false;
            }

            return retVal;
        }

        public static bool PrecalculateRiskByEvent(BiodZebraEntities zebraDbContext, int eventId)
        {
            var retVal = false;

            try
            {
                zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));
                var t = zebraDbContext.usp_ZebraDataRenderSetImportationRiskByEventId(eventId);
            }
            catch (Exception)
            {
                retVal = false;
            }

            return retVal;
        }
    }
}