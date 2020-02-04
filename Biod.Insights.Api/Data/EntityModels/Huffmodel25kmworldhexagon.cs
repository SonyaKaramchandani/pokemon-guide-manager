﻿using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class Huffmodel25kmworldhexagon
    {
        public Huffmodel25kmworldhexagon()
        {
            DiseaseEventDestinationGrid = new HashSet<DiseaseEventDestinationGrid>();
            EventDestinationGrid = new HashSet<EventDestinationGrid>();
            EventDestinationGridHistory = new HashSet<EventDestinationGridHistory>();
            EventDestinationGridSpreadMd = new HashSet<EventDestinationGridSpreadMd>();
            EventDestinationGridV3 = new HashSet<EventDestinationGridV3>();
            GridStation = new HashSet<GridStation>();
        }

        public string GridId { get; set; }
        public int? Population { get; set; }
        public Geometry Shape { get; set; }

        public virtual ICollection<DiseaseEventDestinationGrid> DiseaseEventDestinationGrid { get; set; }
        public virtual ICollection<EventDestinationGrid> EventDestinationGrid { get; set; }
        public virtual ICollection<EventDestinationGridHistory> EventDestinationGridHistory { get; set; }
        public virtual ICollection<EventDestinationGridSpreadMd> EventDestinationGridSpreadMd { get; set; }
        public virtual ICollection<EventDestinationGridV3> EventDestinationGridV3 { get; set; }
        public virtual ICollection<GridStation> GridStation { get; set; }
    }
}