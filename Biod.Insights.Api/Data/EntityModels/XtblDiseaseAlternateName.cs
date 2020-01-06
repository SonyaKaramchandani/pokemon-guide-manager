using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class XtblDiseaseAlternateName
    {
        public int DiseaseId { get; set; }
        public string AlternateName { get; set; }
        public bool? IsColloquial { get; set; }

        public virtual Diseases Disease { get; set; }
    }
}
