using System;
using System.Collections.Generic;

namespace Biod.Zebra.Api.Core.Models
{
    public partial class XtblDiseaseSymptom
    {
        public int DiseaseId { get; set; }
        public int SymptomId { get; set; }
        public string Frequency { get; set; }
        public int? AssociationScore { get; set; }

        public Diseases Disease { get; set; }
        public Symptoms Symptom { get; set; }
    }
}
