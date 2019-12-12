using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class Stations
    {
        public Stations()
        {
            AirportRanking = new HashSet<AirportRanking>();
            DiseaseSourceAirport = new HashSet<DiseaseSourceAirport>();
            EventSourceAirport = new HashSet<EventSourceAirport>();
            GridStation = new HashSet<GridStation>();
            StationDestinationAirportDestinationAirport = new HashSet<StationDestinationAirport>();
            StationDestinationAirportStation = new HashSet<StationDestinationAirport>();
        }

        public int StationId { get; set; }
        public string StationCode { get; set; }
        public string StationGridName { get; set; }
        public string StatioType { get; set; }
        public DateTime? LastModified { get; set; }
        public int? CityId { get; set; }
        public int? GeonameId { get; set; }
        public DateTime? ValidFromDate { get; set; }
        public DateTime? ValidToDate { get; set; }
        public int? CityGeonameId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        public virtual ICollection<AirportRanking> AirportRanking { get; set; }
        public virtual ICollection<DiseaseSourceAirport> DiseaseSourceAirport { get; set; }
        public virtual ICollection<EventSourceAirport> EventSourceAirport { get; set; }
        public virtual ICollection<GridStation> GridStation { get; set; }
        public virtual ICollection<StationDestinationAirport> StationDestinationAirportDestinationAirport { get; set; }
        public virtual ICollection<StationDestinationAirport> StationDestinationAirportStation { get; set; }
    }
}
