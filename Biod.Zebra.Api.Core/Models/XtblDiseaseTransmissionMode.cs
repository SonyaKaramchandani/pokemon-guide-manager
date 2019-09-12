using System;
using System.Collections.Generic;

namespace Biod.Zebra.Api.Core.Models
{
    public partial class XtblDiseaseTransmissionMode
    {
        public int DiseaseId { get; set; }
        public int TransmissionModeId { get; set; }

        public Diseases Disease { get; set; }
        public TransmissionModes TransmissionMode { get; set; }
    }
}
