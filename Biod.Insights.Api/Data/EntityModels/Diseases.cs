using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class Diseases
    {
        public Diseases()
        {
            DiseaseEventDestinationAirport = new HashSet<DiseaseEventDestinationAirport>();
            DiseaseEventDestinationGrid = new HashSet<DiseaseEventDestinationGrid>();
            DiseaseSourceAirport = new HashSet<DiseaseSourceAirport>();
            DiseaseSpeciesIncubation = new HashSet<DiseaseSpeciesIncubation>();
            DiseaseSpeciesSymptomatic = new HashSet<DiseaseSpeciesSymptomatic>();
            XtblArticleLocationDisease = new HashSet<XtblArticleLocationDisease>();
            XtblDiseaseAgents = new HashSet<XtblDiseaseAgents>();
            XtblDiseaseAlternateName = new HashSet<XtblDiseaseAlternateName>();
            XtblDiseaseCustomGroup = new HashSet<XtblDiseaseCustomGroup>();
            XtblDiseaseInterventions = new HashSet<XtblDiseaseInterventions>();
            XtblDiseaseSymptom = new HashSet<XtblDiseaseSymptom>();
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
        public virtual DiseaseEventPrevalence DiseaseEventPrevalence { get; set; }
        public virtual ICollection<DiseaseEventDestinationAirport> DiseaseEventDestinationAirport { get; set; }
        public virtual ICollection<DiseaseEventDestinationGrid> DiseaseEventDestinationGrid { get; set; }
        public virtual ICollection<DiseaseSourceAirport> DiseaseSourceAirport { get; set; }
        public virtual ICollection<DiseaseSpeciesIncubation> DiseaseSpeciesIncubation { get; set; }
        public virtual ICollection<DiseaseSpeciesSymptomatic> DiseaseSpeciesSymptomatic { get; set; }
        public virtual ICollection<XtblArticleLocationDisease> XtblArticleLocationDisease { get; set; }
        public virtual ICollection<XtblDiseaseAgents> XtblDiseaseAgents { get; set; }
        public virtual ICollection<XtblDiseaseAlternateName> XtblDiseaseAlternateName { get; set; }
        public virtual ICollection<XtblDiseaseCustomGroup> XtblDiseaseCustomGroup { get; set; }
        public virtual ICollection<XtblDiseaseInterventions> XtblDiseaseInterventions { get; set; }
        public virtual ICollection<XtblDiseaseSymptom> XtblDiseaseSymptom { get; set; }
        public virtual ICollection<XtblDiseaseTransmissionMode> XtblDiseaseTransmissionMode { get; set; }
        public virtual ICollection<XtblRoleDiseaseRelevance> XtblRoleDiseaseRelevance { get; set; }
        public virtual ICollection<XtblUserDiseaseRelevance> XtblUserDiseaseRelevance { get; set; }
    }
}
