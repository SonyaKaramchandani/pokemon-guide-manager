using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Biod.Insights.Data.EntityModels
{
    public partial class Geonames
    {
        public Geonames()
        {
            EventImportationRisksByGeonameSpreadMd = new HashSet<EventImportationRisksByGeonameSpreadMd>();
            GeonameOutbreakPotential = new HashSet<GeonameOutbreakPotential>();
            InverseAdmin1Geoname = new HashSet<Geonames>();
            InverseCountryGeoname = new HashSet<Geonames>();
            StationsCityGeoname = new HashSet<Stations>();
            StationsGeoname = new HashSet<Stations>();
        }

        public int GeonameId { get; set; }
        public string Name { get; set; }
        public int? LocationType { get; set; }
        public int? Admin1GeonameId { get; set; }
        public int? CountryGeonameId { get; set; }
        public string DisplayName { get; set; }
        public string Alternatenames { get; set; }
        public DateTime ModificationDate { get; set; }
        public string FeatureCode { get; set; }
        public string CountryName { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public long? Population { get; set; }
        public int? SearchSeq2 { get; set; }
        public Geometry Shape { get; set; }
        public decimal? LatPopWeighted { get; set; }
        public decimal? LongPopWeighted { get; set; }

        public virtual Geonames Admin1Geoname { get; set; }
        public virtual Geonames CountryGeoname { get; set; }
        public virtual CountryProvinceShapes CountryProvinceShapes { get; set; }
        public virtual ICollection<EventImportationRisksByGeonameSpreadMd> EventImportationRisksByGeonameSpreadMd { get; set; }
        public virtual ICollection<GeonameOutbreakPotential> GeonameOutbreakPotential { get; set; }
        public virtual ICollection<Geonames> InverseAdmin1Geoname { get; set; }
        public virtual ICollection<Geonames> InverseCountryGeoname { get; set; }
        public virtual ICollection<Stations> StationsCityGeoname { get; set; }
        public virtual ICollection<Stations> StationsGeoname { get; set; }
    }
}
