using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class DiseaseSpeciesSymptomatic
    {
        public int DiseaseId { get; set; }
        public int SpeciesId { get; set; }
        public long? SymptomaticAverageSeconds { get; set; }
        public long? SymptomaticMinimumSeconds { get; set; }
        public long? SymptomaticMaximumSeconds { get; set; }

        public virtual Diseases Disease { get; set; }
        public virtual Species Species { get; set; }
    }
}
