﻿using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class CountryProvinceShapes
    {
        public int GeonameId { get; set; }
        public Geometry Shape { get; set; }
        public Geometry SimplifiedShape { get; set; }
        public int? LocationType { get; set; }

        public virtual Geonames Geoname { get; set; }
    }
}
