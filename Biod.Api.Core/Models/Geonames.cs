using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class Geonames
    {
        public Geonames()
        {
            AirportRanking = new HashSet<AirportRanking>();
            GeonameAlternatenameEng = new HashSet<GeonameAlternatenameEng>();
            XtblArticleLocationDisease = new HashSet<XtblArticleLocationDisease>();
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
        public decimal? LatPopWeighted { get; set; }
        public decimal? LongPopWeighted { get; set; }

        public ICollection<AirportRanking> AirportRanking { get; set; }
        public ICollection<GeonameAlternatenameEng> GeonameAlternatenameEng { get; set; }
        public ICollection<XtblArticleLocationDisease> XtblArticleLocationDisease { get; set; }
        public ICollection<XtblEventLocation> XtblEventLocation { get; set; }
    }
}
