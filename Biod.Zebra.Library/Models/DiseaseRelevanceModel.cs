using Biod.Zebra.Library.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Biod.Zebra.Library.Models.DiseaseRelevance;

namespace Biod.Zebra.Library.Models
{
    public class DiseaseRelevanceModel
    {
        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public int RelevanceId { get; set; }
        public string RelevanceDescription { get; set; }
        public int StateId { get; set; }
        public string StateDescription { get; set; }
        public string Transmission { get; set; }
        public string Alphabetic { get; set; }
    }

    public class QueryId
    {
        public string Id { get; set; }
        public string IdName { get; set; }
        public string IdType { get; set; }//user or role
    }

    public class DiseaseGroup
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public List<DiseaseViewModel> Diseases { get; set; }
    }

    public class DiseaseRelevanceInputJsonModel {
        public string userId { get; set; }
        public string roleId { get; set; }
        public List<DiseaseRelevanceModel> diseaseRelevanceJson { get; set; }

    }
}