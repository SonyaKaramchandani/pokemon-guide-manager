using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Biod.Insights.Api.Data.Models
{
    public partial class GeonamesShapes
    {
        public int GeonameId { get; set; }
        public DateTime? ShapeLastModified { get; set; }
        public Geometry Geom { get; set; }
    }
}
