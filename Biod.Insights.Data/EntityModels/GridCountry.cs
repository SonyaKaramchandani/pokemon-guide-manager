using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class GridCountry
    {
        public string GridId { get; set; }
        public int CountryGeonameId { get; set; }

        public virtual Geonames CountryGeoname { get; set; }
    }
}
