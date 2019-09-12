using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class XtblDiseasePathogens
    {
        public int DiseaseId { get; set; }
        public int PathogenId { get; set; }

        public Diseases Disease { get; set; }
        public Pathogens Pathogen { get; set; }
    }
}
