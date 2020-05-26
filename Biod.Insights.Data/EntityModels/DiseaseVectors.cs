using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class DiseaseVectors
    {
        public DiseaseVectors()
        {
            AcquisitionModes = new HashSet<AcquisitionModes>();
        }

        public int DiseaseVectorId { get; set; }
        public string DiseaseVector { get; set; }
        public string DiseaseVectorCategory { get; set; }

        public virtual ICollection<AcquisitionModes> AcquisitionModes { get; set; }
    }
}
