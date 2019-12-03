using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class UvwCitySubset
    {
        public int GeonameId { get; set; }
        public string AsciiName { get; set; }
        public string DisplayName { get; set; }
        public long? Population { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public string ShapeAsText { get; set; }
        public int? SearchSeq2 { get; set; }
    }
}
