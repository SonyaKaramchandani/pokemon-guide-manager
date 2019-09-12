using System;
using System.Collections.Generic;

namespace Biod.Zebra.Api.Core.Models
{
    public partial class Diseases
    {
        public Diseases()
        {
            XtblArticleLocationDisease = new HashSet<XtblArticleLocationDisease>();
            XtblDiseaseAlternateName = new HashSet<XtblDiseaseAlternateName>();
            XtblDiseasePathogens = new HashSet<XtblDiseasePathogens>();
            XtblDiseasePreventions = new HashSet<XtblDiseasePreventions>();
            XtblDiseaseSymptom = new HashSet<XtblDiseaseSymptom>();
            XtblDiseaseTransmissionMode = new HashSet<XtblDiseaseTransmissionMode>();
        }

        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public DateTime? LastModified { get; set; }
        public decimal? IncubationAverageDays { get; set; }
        public decimal? IncubationMinimumDays { get; set; }
        public decimal? IncubationMaximumDays { get; set; }
        public int? ParentDiseaseId { get; set; }
        public int? MultipleOfIncubationMaximumDays { get; set; }
        public string Pronunciation { get; set; }
        public string SeverityLevel { get; set; }
        public bool? IsChronic { get; set; }
        public string TreatmentAvailable { get; set; }

        public ICollection<XtblArticleLocationDisease> XtblArticleLocationDisease { get; set; }
        public ICollection<XtblDiseaseAlternateName> XtblDiseaseAlternateName { get; set; }
        public ICollection<XtblDiseasePathogens> XtblDiseasePathogens { get; set; }
        public ICollection<XtblDiseasePreventions> XtblDiseasePreventions { get; set; }
        public ICollection<XtblDiseaseSymptom> XtblDiseaseSymptom { get; set; }
        public ICollection<XtblDiseaseTransmissionMode> XtblDiseaseTransmissionMode { get; set; }
    }
}
