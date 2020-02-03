using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class EventSourceDestinationRisk
    {
        public int EventId { get; set; }
        public int SourceAirportId { get; set; }
        public int DestinationAirportId { get; set; }
        public int? Volume { get; set; }
        public decimal? MinProb { get; set; }
        public decimal? MaxProb { get; set; }
        public decimal? MinExpVolume { get; set; }
        public decimal? MaxExpVolume { get; set; }

        public virtual Stations DestinationAirport { get; set; }
        public virtual Event Event { get; set; }
        public virtual Stations SourceAirport { get; set; }
    }
}
