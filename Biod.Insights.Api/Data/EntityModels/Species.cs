using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class Species
    {
        public Species()
        {
            DiseaseSpeciesIncubation = new HashSet<DiseaseSpeciesIncubation>();
            Event = new HashSet<Event>();
            XtblDiseaseTransmissionMode = new HashSet<XtblDiseaseTransmissionMode>();
        }

        public int SpeciesId { get; set; }
        public string SpeciesName { get; set; }

        public virtual ICollection<DiseaseSpeciesIncubation> DiseaseSpeciesIncubation { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<XtblDiseaseTransmissionMode> XtblDiseaseTransmissionMode { get; set; }
    }
}
