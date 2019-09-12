using System;
using System.Collections.Generic;

namespace Biod.Zebra.Api.Core.Models
{
    public partial class GeoNameFeatureCodes
    {
        public int PlaceTypeId { get; set; }
        public string PlaceType { get; set; }
        public string FeatureCode { get; set; }
    }
}
