using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class EventDestinationAirport
    {
        public int EventId { get; set; }
        public int DestinationStationId { get; set; }
        public string StationName { get; set; }
        public string StationCode { get; set; }
        public string CityDisplayName { get; set; }
        public int? Volume { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? MinProb { get; set; }
        public decimal? MaxProb { get; set; }
        public decimal? MinExpVolume { get; set; }
        public decimal? MaxExpVolume { get; set; }

        public Event Event { get; set; }
    }
}
