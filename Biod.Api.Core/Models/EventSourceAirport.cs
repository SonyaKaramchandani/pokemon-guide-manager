using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class EventSourceAirport
    {
        public int EventId { get; set; }
        public int SourceStationId { get; set; }
        public string StationName { get; set; }
        public string StationCode { get; set; }
        public string CityDisplayName { get; set; }
        public string CountryName { get; set; }
        public int? NumCtryAirports { get; set; }
        public int? Volume { get; set; }
        public int? CtryRank { get; set; }
        public int? WorldRank { get; set; }
        public decimal? Probability { get; set; }

        public Event Event { get; set; }
        public Stations SourceStation { get; set; }
    }
}
