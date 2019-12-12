using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class DiseaseSourceAirport
    {
        public int DiseaseId { get; set; }
        public int SourceStationId { get; set; }
        public decimal? Probability { get; set; }

        public virtual Diseases Disease { get; set; }
        public virtual Stations SourceStation { get; set; }
    }
}
