using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Biod.Insights.Api.Data.Models
{
    public partial class CountryProvinceShapesBak
    {
        public int GeonameId { get; set; }
        public Geometry Shape { get; set; }
        public Geometry SimplifiedShape { get; set; }
        public int? LocationType { get; set; }
    }
}
