using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class DiseaseSpeciesIncubation
    {
        public int DiseaseId { get; set; }
        public int SpeciesId { get; set; }
        public long? IncubationAverageSeconds { get; set; }
        public long? IncubationMinimumSeconds { get; set; }
        public long? IncubationMaximumSeconds { get; set; }

        public virtual Diseases Disease { get; set; }
        public virtual Species Species { get; set; }
    }
}
