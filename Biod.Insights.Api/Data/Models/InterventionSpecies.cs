using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class InterventionSpecies
    {
        public int InterventionId { get; set; }
        public int SpeciesId { get; set; }
        public decimal? RiskReduction { get; set; }
        public string Duration { get; set; }

        public virtual Interventions Intervention { get; set; }
        public virtual Species Species { get; set; }
    }
}
