using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class UserTypeDiseaseRelevances
    {
        public Guid UserTypeId { get; set; }
        public int DiseaseId { get; set; }
        public int RelevanceId { get; set; }

        public virtual Diseases Disease { get; set; }
        public virtual RelevanceType Relevance { get; set; }
        public virtual UserTypes UserType { get; set; }
    }
}
