﻿using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class Symptoms
    {
        public Symptoms()
        {
            XtblDiseaseSymptom = new HashSet<XtblDiseaseSymptom>();
        }

        public int SymptomId { get; set; }
        public string Symptom { get; set; }
        public int? SystemId { get; set; }
        public string SymptomDefinition { get; set; }
        public string DefinitionSource { get; set; }
        public DateTime? LastModified { get; set; }

        public virtual Systems System { get; set; }
        public virtual ICollection<XtblDiseaseSymptom> XtblDiseaseSymptom { get; set; }
    }
}
