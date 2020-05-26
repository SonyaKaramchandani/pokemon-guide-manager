using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class RelevanceState
    {
        public RelevanceState()
        {
            XtblUserDiseaseRelevance = new HashSet<XtblUserDiseaseRelevance>();
        }

        public int StateId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<XtblUserDiseaseRelevance> XtblUserDiseaseRelevance { get; set; }
    }
}
