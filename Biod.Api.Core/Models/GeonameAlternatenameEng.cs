using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class GeonameAlternatenameEng
    {
        public int GeonameId { get; set; }
        public string AlternatenameEng { get; set; }
        public int? LocationType { get; set; }

        public Geonames Geoname { get; set; }
    }
}
