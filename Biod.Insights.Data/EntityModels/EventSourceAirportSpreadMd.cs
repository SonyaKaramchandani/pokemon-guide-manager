using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class EventSourceAirportSpreadMd
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
        public double? MinCaseOverPop { get; set; }
        public double? MaxCaseOverPop { get; set; }
        public double? MinPrevalence { get; set; }
        public double? MaxPrevalence { get; set; }
        public decimal? MinProb { get; set; }
        public decimal? MaxProb { get; set; }
        public decimal? MinExpVolume { get; set; }
        public decimal? MaxExpVolume { get; set; }

        public virtual Event Event { get; set; }
        public virtual Stations SourceStation { get; set; }
    }
}
