﻿using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class GeoNameFeatureCodes
    {
        public int PlaceTypeId { get; set; }
        public string PlaceType { get; set; }
        public string FeatureCode { get; set; }
    }
}
