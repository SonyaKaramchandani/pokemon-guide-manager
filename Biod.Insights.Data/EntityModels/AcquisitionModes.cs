using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class AcquisitionModes
    {
        public AcquisitionModes()
        {
            XtblDiseaseAcquisitionMode = new HashSet<XtblDiseaseAcquisitionMode>();
        }

        public int AcquisitionModeId { get; set; }
        public int DiseaseVectorId { get; set; }
        public int TransferModalityId { get; set; }
        public int? Multiplier { get; set; }
        public string AcquisitionModeLabel { get; set; }
        public string AcquisitionModeDefinitionLabel { get; set; }

        public virtual DiseaseVectors DiseaseVector { get; set; }
        public virtual TransferModality TransferModality { get; set; }
        public virtual ICollection<XtblDiseaseAcquisitionMode> XtblDiseaseAcquisitionMode { get; set; }
    }
}
