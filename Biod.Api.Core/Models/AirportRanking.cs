using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class AirportRanking
    {
        public int StationId { get; set; }
        public int WorldRank { get; set; }
        public int NumWorldAirports { get; set; }
        public int CtryRank { get; set; }
        public int NumCtryAirports { get; set; }
        public int CtryGeonameId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? ValidTo { get; set; }
        public int InboundVolume { get; set; }
        public int OutboundVolume { get; set; }
        public DateTime LastModified { get; set; }

        public Geonames CtryGeoname { get; set; }
        public Stations Station { get; set; }
    }
}
