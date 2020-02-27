using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class ActiveGeonames
    {
        public ActiveGeonames()
        {
            XtblEventLocation = new HashSet<XtblEventLocation>();
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

        public virtual ICollection<XtblEventLocation> XtblEventLocation { get; set; }
    }
}
