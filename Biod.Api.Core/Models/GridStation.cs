using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class GridStation
    {
        public string GridId { get; set; }
        public int StationId { get; set; }
        public decimal? Probability { get; set; }
        public DateTime ValidFromDate { get; set; }
        public DateTime? LastModified { get; set; }

        public Huffmodel25kmworldhexagon Grid { get; set; }
        public Stations Station { get; set; }
    }
}
