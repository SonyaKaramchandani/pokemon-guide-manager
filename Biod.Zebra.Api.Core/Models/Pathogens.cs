using System;
using System.Collections.Generic;

namespace Biod.Zebra.Api.Core.Models
{
    public partial class Pathogens
    {
        public Pathogens()
        {
            XtblDiseasePathogens = new HashSet<XtblDiseasePathogens>();
        }

        public int PathogenId { get; set; }
        public string Pathogen { get; set; }
        public int PathogenTypeId { get; set; }

        public PathogenTypes PathogenType { get; set; }
        public ICollection<XtblDiseasePathogens> XtblDiseasePathogens { get; set; }
    }
}
