using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class RelevanceType
    {
        public RelevanceType()
        {
            UserTypeDiseaseRelevances = new HashSet<UserTypeDiseaseRelevances>();
            XtblUserDiseaseRelevance = new HashSet<XtblUserDiseaseRelevance>();
        }

        public int RelevanceId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserTypeDiseaseRelevances> UserTypeDiseaseRelevances { get; set; }
        public virtual ICollection<XtblUserDiseaseRelevance> XtblUserDiseaseRelevance { get; set; }
    }
}
