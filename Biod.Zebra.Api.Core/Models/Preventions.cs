using System;
using System.Collections.Generic;

namespace Biod.Zebra.Api.Core.Models
{
    public partial class Preventions
    {
        public Preventions()
        {
            XtblDiseasePreventions = new HashSet<XtblDiseasePreventions>();
        }

        public int PreventionId { get; set; }
        public string PreventionType { get; set; }
        public bool? Oral { get; set; }
        public decimal? RiskReduction { get; set; }
        public string Duration { get; set; }

        public ICollection<XtblDiseasePreventions> XtblDiseasePreventions { get; set; }
    }
}
