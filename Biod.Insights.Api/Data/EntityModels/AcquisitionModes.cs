using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class AcquisitionModes
    {
        public AcquisitionModes()
        {
            XtblDiseaseAcquisitionMode = new HashSet<XtblDiseaseAcquisitionMode>();
        }

        public int AcquisitionModeId { get; set; }
        public string AcquisitionModeLabel { get; set; }
        public string DiseaseSource { get; set; }
        public string ModalityName { get; set; }
        public string ModalityInsightsDisplay { get; set; }
        public int? Mulitplier { get; set; }
        public bool? IsDirect { get; set; }

        public virtual ICollection<XtblDiseaseAcquisitionMode> XtblDiseaseAcquisitionMode { get; set; }
    }
}
