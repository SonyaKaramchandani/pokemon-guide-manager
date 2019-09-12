using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class Huffmodel25kmworldhexagon
    {
        public Huffmodel25kmworldhexagon()
        {
            EventDestinationGridV3 = new HashSet<EventDestinationGridV3>();
            GridStation = new HashSet<GridStation>();
        }

        public string GridId { get; set; }
        public int? Population { get; set; }

        public ICollection<EventDestinationGridV3> EventDestinationGridV3 { get; set; }
        public ICollection<GridStation> GridStation { get; set; }
    }
}
