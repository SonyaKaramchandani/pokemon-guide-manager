using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Dynamic;
using Biod.Zebra.Library.Models.DiseaseRelevance;
using Biod.Zebra.Library.EntityModels.Zebra;

namespace Biod.Zebra.Library.Models
{
    public class DiseaseRelevanceViewModel
    {
        public DiseaseRelevanceViewModel() { }
        public DiseaseRelevanceViewModel(string userId, string roleId, int groupType = 1)
        {
            BiodZebraEntities zebraDbContext = new BiodZebraEntities();
            zebraDbContext.Database.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ApiTimeout"));

            var tempDisRelList = new List<DiseaseRelevanceModel>();
            var tempId = new QueryId();

            if (!String.IsNullOrEmpty(userId)) {
                var queryResult = zebraDbContext.uvw_DiseaseRelevanceByUser.Where(x => x.UserId == userId);

                if (queryResult.Any()) {
                    tempId = new QueryId()
                    {
                        Id = queryResult.First().UserId,
                        IdName = queryResult.First().UserEmail,
                        IdType = "user"
                    };
                }

                foreach (var i in queryResult) {
                    var dr = new DiseaseRelevanceModel() {
                        DiseaseId = i.DiseaseId,
                        DiseaseName = i.DiseaseName,
                        RelevanceId = i.RelevanceId,
                        RelevanceDescription = i.RelevanceDescription,
                        StateId = i.StateId,
                        StateDescription = i.StateDescription
                    };
                    tempDisRelList.Add(dr);
                }
            }
            else if (!String.IsNullOrEmpty(roleId))
            {
                var queryResult = zebraDbContext.uvw_DiseaseRelevanceByRole.Where(x => x.RoleId == roleId);

                if (queryResult.Any())
                {
                    tempId = new QueryId()
                    {
                        Id = queryResult.First().RoleId,
                        IdName = queryResult.First().RoleName,
                        IdType = "user"
                    };
                }

                foreach (var i in queryResult)
                {
                    var dr = new DiseaseRelevanceModel()
                    {
                        DiseaseId = i.DiseaseId,
                        DiseaseName = i.DiseaseName,
                        RelevanceId = i.RelevanceId,
                        StateId = i.StateId,
                        StateDescription = i.StateDescription
                    };
                    tempDisRelList.Add(dr);
                }
            }

            //=============================================

            var tempDiseases = zebraDbContext.usp_ZebraDashboardGetDiseases().Select(x => new DiseaseViewModel { DiseaseId = x.DiseaseId, DiseaseName = x.DiseaseName }).ToList();

            //=============================================

            var tempGroups = new List<DiseaseGroup>();
            var queryGroups = zebraDbContext.usp_ZebraDiseaseGetDiseasesByGroupType(groupType).ToList();
            if (queryGroups.Any())
            {
                foreach (var i in queryGroups) { 
                    var dg = new DiseaseGroup();
                    dg.GroupId = Convert.ToInt32(i.GroupId);
                    dg.GroupName = i.GroupName;
                    dg.Diseases = new List<DiseaseViewModel>();
                    foreach (var j in i.DiseaseIds.Split(','))
                    {
                        dg.Diseases.Add(tempDiseases.Where(x => x.DiseaseId == Convert.ToInt32(j.Trim())).FirstOrDefault());
                    }
                    tempGroups.Add(dg);
                }
            }
            else //alphabetical, just one group
            {
                var dg = new DiseaseGroup();
                dg.GroupId = 1;
                dg.GroupName = "Alphabetical";
                dg.Diseases = tempDiseases.OrderBy(x => x.DiseaseName).ToList();
                tempGroups.Add(dg);
            }

            //=============================================

            InputId = tempId;
            DiseaseRelevanceList = tempDisRelList;
            RelevanceTypeList = zebraDbContext.RelevanceTypes.ToList();
            RelevanceStateList = zebraDbContext.RelevanceStates.ToList();
            DiseaseList = tempDiseases;
            DiseaseGroups = tempGroups;
            
        }

        public QueryId InputId { get; set; }
        public List<DiseaseRelevanceModel> DiseaseRelevanceList { get; set; }
        public List<RelevanceType> RelevanceTypeList { get; set; }
        public List<RelevanceState> RelevanceStateList { get; set; }
        public List<DiseaseViewModel> DiseaseList { get; set; }
        public List<DiseaseGroup> DiseaseGroups { get; set; }
    }
}