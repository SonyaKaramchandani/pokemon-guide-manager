using System;
using System.Collections.Generic;

namespace Biod.Zebra.Api.Core.Models
{
    public partial class TransmissionModes
    {
        public TransmissionModes()
        {
            XtblDiseaseTransmissionMode = new HashSet<XtblDiseaseTransmissionMode>();
        }

        public int TransmissionModeId { get; set; }
        public string TransmissionMode { get; set; }

        public ICollection<XtblDiseaseTransmissionMode> XtblDiseaseTransmissionMode { get; set; }
    }
}
