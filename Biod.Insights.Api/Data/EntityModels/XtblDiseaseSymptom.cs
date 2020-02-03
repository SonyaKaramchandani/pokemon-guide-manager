using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class XtblDiseaseSymptom
    {
        public int DiseaseId { get; set; }
        public int SpeciesId { get; set; }
        public int SymptomId { get; set; }
        public string Frequency { get; set; }
        public int? AssociationScore { get; set; }

        public virtual Diseases Disease { get; set; }
        public virtual Species Species { get; set; }
        public virtual Symptoms Symptom { get; set; }
    }
}
