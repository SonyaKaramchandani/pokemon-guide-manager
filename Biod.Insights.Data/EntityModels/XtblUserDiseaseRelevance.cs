using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class XtblUserDiseaseRelevance
    {
        public string UserId { get; set; }
        public int DiseaseId { get; set; }
        public int RelevanceId { get; set; }
        public int StateId { get; set; }

        public virtual Diseases Disease { get; set; }
        public virtual RelevanceType Relevance { get; set; }
        public virtual RelevanceState State { get; set; }
        public virtual UserProfile User { get; set; }
    }
}
