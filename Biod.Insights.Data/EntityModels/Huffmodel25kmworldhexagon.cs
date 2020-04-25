using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Biod.Insights.Data.EntityModels
{
    public partial class Huffmodel25kmworldhexagon
    {
        public Huffmodel25kmworldhexagon()
        {
            EventSourceGridSpreadMd = new HashSet<EventSourceGridSpreadMd>();
            GridStation = new HashSet<GridStation>();
        }

        public string GridId { get; set; }
        public int? Population { get; set; }
        public Geometry Shape { get; set; }

        public virtual ICollection<EventSourceGridSpreadMd> EventSourceGridSpreadMd { get; set; }
        public virtual ICollection<GridStation> GridStation { get; set; }
    }
}
