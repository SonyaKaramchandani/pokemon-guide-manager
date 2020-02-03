using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class UvwDiseaseRelevanceByUser
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public int RelevanceId { get; set; }
        public string RelevanceDescription { get; set; }
        public int StateId { get; set; }
        public string StateDescription { get; set; }
    }
}
