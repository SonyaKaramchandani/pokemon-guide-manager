using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class TransmissionModes
    {
        public TransmissionModes()
        {
            XtblDiseaseTransmissionMode = new HashSet<XtblDiseaseTransmissionMode>();
        }

        public int TransmissionModeId { get; set; }
        public string TransmissionMode { get; set; }
        public string DisplayName { get; set; }

        public virtual ICollection<XtblDiseaseTransmissionMode> XtblDiseaseTransmissionMode { get; set; }
    }
}
