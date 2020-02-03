using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class DiseaseEventDestinationGrid
    {
        public int DiseaseId { get; set; }
        public string GridId { get; set; }

        public virtual Diseases Disease { get; set; }
        public virtual Huffmodel25kmworldhexagon Grid { get; set; }
    }
}
