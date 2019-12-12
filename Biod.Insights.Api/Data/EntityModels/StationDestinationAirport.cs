using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class StationDestinationAirport
    {
        public int StationId { get; set; }
        public int DestinationAirportId { get; set; }
        public DateTime ValidFromDate { get; set; }
        public int? Volume { get; set; }

        public virtual Stations DestinationAirport { get; set; }
        public virtual Stations Station { get; set; }
    }
}
