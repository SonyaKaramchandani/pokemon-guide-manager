using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class XtblDiseaseInterventions
    {
        public int DiseaseId { get; set; }
        public int SpeciesId { get; set; }
        public int InterventionId { get; set; }

        public virtual Diseases Disease { get; set; }
        public virtual Interventions Intervention { get; set; }
    }
}
