using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class GeonameAlternatenameEng
    {
        public int GeonameId { get; set; }
        public string AlternatenameEng { get; set; }
        public int? LocationType { get; set; }
        public int AlternateNameId { get; set; }

        public virtual Geonames Geoname { get; set; }
    }
}
