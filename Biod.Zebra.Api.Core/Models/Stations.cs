using System;
using System.Collections.Generic;

namespace Biod.Zebra.Api.Core.Models
{
    public partial class Stations
    {
        public Stations()
        {
            GridStation = new HashSet<GridStation>();
            StationDestinationAirportDestinationAirport = new HashSet<StationDestinationAirport>();
            StationDestinationAirportStation = new HashSet<StationDestinationAirport>();
        }

        public int StationId { get; set; }
        public string StationCode { get; set; }
        public string StateName { get; set; }
        public string StatioType { get; set; }
        public DateTime? LastModified { get; set; }
        public int? CityId { get; set; }
        public int? GeonameId { get; set; }
        public DateTime? ValidFromDate { get; set; }
        public DateTime? ValidToDate { get; set; }

        public ICollection<GridStation> GridStation { get; set; }
        public ICollection<StationDestinationAirport> StationDestinationAirportDestinationAirport { get; set; }
        public ICollection<StationDestinationAirport> StationDestinationAirportStation { get; set; }
    }
}
