using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class StationDestinationAirport
    {
        public int StationId { get; set; }
        public int DestinationAirportId { get; set; }
        public DateTime ValidFromDate { get; set; }
        public int? Volume { get; set; }

        public Stations DestinationAirport { get; set; }
        public Stations Station { get; set; }
    }
}
