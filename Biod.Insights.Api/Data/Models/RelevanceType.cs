using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class RelevanceType
    {
        public RelevanceType()
        {
            XtblRoleDiseaseRelevance = new HashSet<XtblRoleDiseaseRelevance>();
            XtblUserDiseaseRelevance = new HashSet<XtblUserDiseaseRelevance>();
        }

        public int RelevanceId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<XtblRoleDiseaseRelevance> XtblRoleDiseaseRelevance { get; set; }
        public virtual ICollection<XtblUserDiseaseRelevance> XtblUserDiseaseRelevance { get; set; }
    }
}
