using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using Biod.Zebra.Library.EntityModels.Zebra;
using Biod.Zebra.Library.Infrastructures;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Biod.Zebra.Library.Models.DiseaseRelevance
{
    public class DiseaseRelevanceViewModel
    {
        private DiseaseRelevanceViewModel(BiodZebraEntities dbContext)
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
            var userTypes = dbContext.UserTypes.ToList();

            foreach (var userType in userTypes)
            {
                if (!RolesMap.ContainsKey(userType.Id.ToString()))
                    RolesMap.Add(userType.Id.ToString(), new RelevanceViewModel
                    {
                        Id = userType.Id.ToString(),
                        Name = userType.Name,
                        Description = userType.NotificationDescription,
                        DiseaseSetting = new Dictionary<int, RelevanceViewModel.DiseaseRelevanceSettingViewModel>()
                    });

                var diseaseSetting = RolesMap[userType.Id.ToString()].DiseaseSetting;
                var diseaseRelevances = diseaseRelevanceGroupedByRole.FirstOrDefault(g => g.Key == userType.Id.ToString());
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

        public static DiseaseRelevanceViewModel GetDiseaseRelevanceAdminViewModel(BiodZebraEntities dbContext)
        {
            var model = new DiseaseRelevanceViewModel(dbContext);

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

        public static DiseaseRelevanceViewModel GetDiseaseRelevanceSettingViewModel(BiodZebraEntities dbContext)
        {
            var model = new DiseaseRelevanceViewModel(dbContext);

            // Add the Mode of Transmission grouping 
            model.DiseaseGroupsMap.Add(1, new DiseaseGroupTypeViewModel
            {
                Id = 1,
                Name = "Mode of acquisition",
                DiseaseGroups = dbContext.Xtbl_Disease_AcquisitionMode
                    .Where(x =>
                            x.AcquisitionModeRank == 1     // 1 is Common
                            || x.AcquisitionModeRank == 2  // 2 is Uncommon
                    )
                    .Select(x => new
                    {
                        x.AcquisitionModeId,
                        Name = x.AcquisitionMode.AcquisitionModeLabel,
                        x.DiseaseId
                    })
                    .AsEnumerable()
                    .GroupBy(x => new {x.AcquisitionModeId, x.Name})
                    .Select(g => new DiseaseGroupViewModel
                    {
                        Id = g.Key.AcquisitionModeId,
                        Name = g.Key.Name,
                        DiseaseIds = g.Select(x => x.DiseaseId).ToList()
                    })
                    .OrderBy(g => g.Name)
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