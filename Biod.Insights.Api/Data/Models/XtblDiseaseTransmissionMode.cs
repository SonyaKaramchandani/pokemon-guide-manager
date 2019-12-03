using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class XtblDiseaseTransmissionMode
    {
        public int DiseaseId { get; set; }
        public int SpeciesId { get; set; }
        public int TransmissionModeId { get; set; }

        public virtual Diseases Disease { get; set; }
        public virtual Species Species { get; set; }
        public virtual TransmissionModes TransmissionMode { get; set; }
    }
}
