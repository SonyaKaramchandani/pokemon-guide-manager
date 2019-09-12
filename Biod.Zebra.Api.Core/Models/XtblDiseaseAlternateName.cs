using System;
using System.Collections.Generic;

namespace Biod.Zebra.Api.Core.Models
{
    public partial class XtblDiseaseAlternateName
    {
        public int DiseaseId { get; set; }
        public string AlternateName { get; set; }
        public bool? IsColloquial { get; set; }

        public Diseases Disease { get; set; }
    }
}
