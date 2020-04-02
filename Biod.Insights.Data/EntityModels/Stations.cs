using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class Stations
    {
        public Stations()
        {
            EventDestinationAirportSpreadMd = new HashSet<EventDestinationAirportSpreadMd>();
            EventSourceAirportSpreadMd = new HashSet<EventSourceAirportSpreadMd>();
            GridStation = new HashSet<GridStation>();
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

        public virtual Geonames CityGeoname { get; set; }
        public virtual Geonames Geoname { get; set; }
        public virtual ICollection<EventDestinationAirportSpreadMd> EventDestinationAirportSpreadMd { get; set; }
        public virtual ICollection<EventSourceAirportSpreadMd> EventSourceAirportSpreadMd { get; set; }
        public virtual ICollection<GridStation> GridStation { get; set; }
    }
}
