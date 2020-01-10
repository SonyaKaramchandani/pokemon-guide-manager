using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class Species
    {
        public Species()
        {
            DiseaseSpeciesIncubation = new HashSet<DiseaseSpeciesIncubation>();
            DiseaseSpeciesSymptomatic = new HashSet<DiseaseSpeciesSymptomatic>();
            Event = new HashSet<Event>();
            InterventionSpecies = new HashSet<InterventionSpecies>();
            XtblDiseaseAcquisitionMode = new HashSet<XtblDiseaseAcquisitionMode>();
            XtblDiseaseSymptom = new HashSet<XtblDiseaseSymptom>();
            XtblDiseaseTransmissionMode = new HashSet<XtblDiseaseTransmissionMode>();
        }

        public int SpeciesId { get; set; }
        public string SpeciesName { get; set; }

        public virtual ICollection<DiseaseSpeciesIncubation> DiseaseSpeciesIncubation { get; set; }
        public virtual ICollection<DiseaseSpeciesSymptomatic> DiseaseSpeciesSymptomatic { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<InterventionSpecies> InterventionSpecies { get; set; }
        public virtual ICollection<XtblDiseaseAcquisitionMode> XtblDiseaseAcquisitionMode { get; set; }
        public virtual ICollection<XtblDiseaseSymptom> XtblDiseaseSymptom { get; set; }
        public virtual ICollection<XtblDiseaseTransmissionMode> XtblDiseaseTransmissionMode { get; set; }
    }
}
