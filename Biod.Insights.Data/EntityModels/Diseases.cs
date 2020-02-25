using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class Diseases
    {
        public Diseases()
        {
            DiseaseSpeciesIncubation = new HashSet<DiseaseSpeciesIncubation>();
            Event = new HashSet<Event>();
            GeonameOutbreakPotential = new HashSet<GeonameOutbreakPotential>();
            InverseParentDisease = new HashSet<Diseases>();
            XtblDiseaseAcquisitionMode = new HashSet<XtblDiseaseAcquisitionMode>();
            XtblDiseaseAgents = new HashSet<XtblDiseaseAgents>();
            XtblDiseaseInterventions = new HashSet<XtblDiseaseInterventions>();
            XtblDiseaseTransmissionMode = new HashSet<XtblDiseaseTransmissionMode>();
            XtblRoleDiseaseRelevance = new HashSet<XtblRoleDiseaseRelevance>();
            XtblUserDiseaseRelevance = new HashSet<XtblUserDiseaseRelevance>();
        }

        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public string DiseaseType { get; set; }
        public DateTime? LastModified { get; set; }
        public int? ParentDiseaseId { get; set; }
        public string Pronunciation { get; set; }
        public string SeverityLevel { get; set; }
        public bool? IsChronic { get; set; }
        public string TreatmentAvailable { get; set; }
        public string BiosecurityRisk { get; set; }
        public int? OutbreakPotentialAttributeId { get; set; }
        public bool? IsZoonotic { get; set; }

        public virtual BiosecurityRisk BiosecurityRiskNavigation { get; set; }
        public virtual Diseases ParentDisease { get; set; }
        public virtual ICollection<DiseaseSpeciesIncubation> DiseaseSpeciesIncubation { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<GeonameOutbreakPotential> GeonameOutbreakPotential { get; set; }
        public virtual ICollection<Diseases> InverseParentDisease { get; set; }
        public virtual ICollection<XtblDiseaseAcquisitionMode> XtblDiseaseAcquisitionMode { get; set; }
        public virtual ICollection<XtblDiseaseAgents> XtblDiseaseAgents { get; set; }
        public virtual ICollection<XtblDiseaseInterventions> XtblDiseaseInterventions { get; set; }
        public virtual ICollection<XtblDiseaseTransmissionMode> XtblDiseaseTransmissionMode { get; set; }
        public virtual ICollection<XtblRoleDiseaseRelevance> XtblRoleDiseaseRelevance { get; set; }
        public virtual ICollection<XtblUserDiseaseRelevance> XtblUserDiseaseRelevance { get; set; }
    }
}
