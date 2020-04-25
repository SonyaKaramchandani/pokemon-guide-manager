using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class Species
    {
        public Species()
        {
            DiseaseSpeciesIncubation = new HashSet<DiseaseSpeciesIncubation>();
            DiseaseSpeciesSymptomatic = new HashSet<DiseaseSpeciesSymptomatic>();
            Event = new HashSet<Event>();
            XtblDiseaseAcquisitionMode = new HashSet<XtblDiseaseAcquisitionMode>();
            XtblDiseaseTransmissionMode = new HashSet<XtblDiseaseTransmissionMode>();
        }

        public int SpeciesId { get; set; }
        public string SpeciesName { get; set; }

        public virtual ICollection<DiseaseSpeciesIncubation> DiseaseSpeciesIncubation { get; set; }
        public virtual ICollection<DiseaseSpeciesSymptomatic> DiseaseSpeciesSymptomatic { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<XtblDiseaseAcquisitionMode> XtblDiseaseAcquisitionMode { get; set; }
        public virtual ICollection<XtblDiseaseTransmissionMode> XtblDiseaseTransmissionMode { get; set; }
    }
}
