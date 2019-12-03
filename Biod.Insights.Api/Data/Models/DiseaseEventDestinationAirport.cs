using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class DiseaseEventDestinationAirport
    {
        public int DiseaseId { get; set; }
        public int DestinationStationId { get; set; }
        public int? Volume { get; set; }
        public decimal? MinProb { get; set; }
        public decimal? MaxProb { get; set; }
        public decimal? MinExpVolume { get; set; }
        public decimal? MaxExpVolume { get; set; }

        public virtual Diseases Disease { get; set; }
    }
}
