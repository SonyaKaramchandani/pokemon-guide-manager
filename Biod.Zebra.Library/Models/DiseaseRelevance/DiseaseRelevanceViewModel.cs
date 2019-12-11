using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Infrastructures;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Biod.Zebra.Library.Models.DiseaseRelevance
{
    public class DiseaseRelevanceViewModel
    {
        private DiseaseRelevanceViewModel(BiodZebraEntities dbContext, RoleManager<IdentityRole> roleManager)
        {
            RelevanceTypes = dbContext.RelevanceTypes
                .OrderBy(t => t.RelevanceId)
                .Select(t => new RelevanceTypeViewModel {Id = t.RelevanceId, Name = t.Description})
                .ToList();
            Diseases = dbContext.usp_ZebraDashboardGetDiseases()
                .OrderBy(d => d.DiseaseName)
                .Select(d => new DiseaseViewModel {DiseaseId = d.DiseaseId, DiseaseName = d.DiseaseName})
                .ToList();
            DiseaseGroupsMap = new Dictionary<int, DiseaseGroupTypeViewModel>();
            RolesMap = new Dictionary<string, RelevanceViewModel>();

            // Populate the Roles Map with the settings for each role
            var diseaseRelevanceGroupedByRole = dbContext.Xtbl_Role_Disease_Relevance.GroupBy(r => r.RoleId).ToList();
            var publicRoleNames = new HashSet<string>(new CustomRolesFilter(dbContext).GetPublicRoleNames());
            var roles = dbContext.AspNetRoles.Where(r => publicRoleNames.Contains(r.Name)).ToList();

            foreach (var role in roles)
            {
                if (!RolesMap.ContainsKey(role.Id))
                    RolesMap.Add(role.Id, new RelevanceViewModel
                    {
                        Id = role.Id,
                        Name = role.Name,
                        Description = role.NotificationDescription,
                        DiseaseSetting = new Dictionary<int, RelevanceViewModel.DiseaseRelevanceSettingViewModel>()
                    });

                var diseaseSetting = RolesMap[role.Id].DiseaseSetting;
                var diseaseRelevances = diseaseRelevanceGroupedByRole.FirstOrDefault(g => g.Key == role.Id);
                if (diseaseRelevances != null)
                    foreach (var diseaseRelevance in diseaseRelevances)
                        diseaseSetting.Add(diseaseRelevance.DiseaseId,
                            new RelevanceViewModel.DiseaseRelevanceSettingViewModel
                            {
                                DiseaseId = diseaseRelevance.DiseaseId,
                                DiseaseName = Diseases.FirstOrDefault(d => d.DiseaseId == diseaseRelevance.DiseaseId)
                                    ?.DiseaseName,
                                RelevanceType = diseaseRelevance.RelevanceId,
                                State = null
                            });
            }
        }

        public List<RelevanceTypeViewModel> RelevanceTypes { get; set; }

        public List<DiseaseViewModel> Diseases { get; set; }

        public Dictionary<int, DiseaseGroupTypeViewModel> DiseaseGroupsMap { get; set; }

        public Dictionary<string, RelevanceViewModel> RolesMap { get; set; }

        public static DiseaseRelevanceViewModel GetDiseaseRelevanceAdminViewModel(BiodZebraEntities dbContext, RoleManager<IdentityRole> roleManager)
        {
            var model = new DiseaseRelevanceViewModel(dbContext, roleManager);

            // Admin Model only has 1 group type, which is the disease groups defined by Admins
            model.DiseaseGroupsMap.Add(0, new DiseaseGroupTypeViewModel
            {
                Id = 0,
                Name = "",
                DiseaseGroups = dbContext.CustomGroups.Include(cg => cg.Xtbl_Disease_CustomGroup).Select(g =>
                    new DiseaseGroupViewModel
                    {
                        Id = g.GroupId,
                        Name = g.GroupName,
                        DiseaseIds = g.Xtbl_Disease_CustomGroup.Select(x => x.DiseaseId).ToList()
                    }).ToList()
            });

            return model;
        }

        public static DiseaseRelevanceViewModel GetDiseaseRelevanceSettingViewModel(BiodZebraEntities dbContext, RoleManager<IdentityRole> roleManager)
        {
            var model = new DiseaseRelevanceViewModel(dbContext, roleManager);

            // Add the Mode of Transmission grouping 
            model.DiseaseGroupsMap.Add(1, new DiseaseGroupTypeViewModel
            {
                Id = 1,
                Name = "Mode of transmission",
                DiseaseGroups = dbContext.usp_ZebraDiseaseGetDiseasesByGroupType(1)
                    .Select(g => new DiseaseGroupViewModel
                    {
                        Id = g.GroupId ?? 0,
                        Name = g.GroupName,
                        DiseaseIds = g.DiseaseIds.Split(',').Select(d => Convert.ToInt32(d.Trim())).ToList()
                    })
                    .ToList()
            });

            // Add the Alphabetical grouping
            model.DiseaseGroupsMap.Add(2, new DiseaseGroupTypeViewModel
            {
                Id = 2,
                Name = "Alphabetical",
                DiseaseGroups = model.Diseases.GroupBy(d => d.DiseaseName.Substring(0, 1).ToUpper(),
                        (letter, subList) => new
                        {
                            Letter = letter,
                            SubList = subList.OrderBy(d => d.DiseaseName).Select(d => d.DiseaseId).ToList()
                        })
                    .OrderBy(x => x.Letter)
                    .Select((x, i) => new DiseaseGroupViewModel
                    {
                        Id = i,
                        Name = x.Letter,
                        DiseaseIds = x.SubList
                    })
                    .ToList()
            });

            return model;
        }
    }
}