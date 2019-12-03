using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class DiseaseEventPrevalence
    {
        public int DiseaseId { get; set; }
        public double? MinPrevelance { get; set; }
        public double? MaxPrevelance { get; set; }
        public int EventMonth { get; set; }

        public virtual Diseases Disease { get; set; }
    }
}
