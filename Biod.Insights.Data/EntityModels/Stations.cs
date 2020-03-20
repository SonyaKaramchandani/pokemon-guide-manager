using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class Stations
    {
        public Stations()
        {
            EventDestinationAirport = new HashSet<EventDestinationAirport>();
            EventSourceAirport = new HashSet<EventSourceAirport>();
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
        public virtual ICollection<EventDestinationAirport> EventDestinationAirport { get; set; }
        public virtual ICollection<EventSourceAirport> EventSourceAirport { get; set; }
    }
}
