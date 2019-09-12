using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class XtblDiseasePreventions
    {
        public int DiseaseId { get; set; }
        public int PreventionId { get; set; }

        public Diseases Disease { get; set; }
        public Preventions Prevention { get; set; }
    }
}
