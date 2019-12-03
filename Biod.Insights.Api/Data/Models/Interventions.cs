using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class Interventions
    {
        public Interventions()
        {
            InterventionSpecies = new HashSet<InterventionSpecies>();
            XtblDiseaseInterventions = new HashSet<XtblDiseaseInterventions>();
        }

        public int InterventionId { get; set; }
        public string InterventionType { get; set; }
        public bool? Oral { get; set; }
        public string DisplayName { get; set; }

        public virtual ICollection<InterventionSpecies> InterventionSpecies { get; set; }
        public virtual ICollection<XtblDiseaseInterventions> XtblDiseaseInterventions { get; set; }
    }
}
