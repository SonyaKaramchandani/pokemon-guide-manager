using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class XtblDiseaseAcquisitionMode
    {
        public int DiseaseId { get; set; }
        public int SpeciesId { get; set; }
        public int AcquisitionModeId { get; set; }
        public int AcquisitionModeRank { get; set; }

        public virtual AcquisitionModes AcquisitionMode { get; set; }
        public virtual Diseases Disease { get; set; }
        public virtual Species Species { get; set; }
    }
}
