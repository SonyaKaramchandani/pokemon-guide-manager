using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class EventSourceGridSpreadMd
    {
        public int EventId { get; set; }
        public string GridId { get; set; }
        public int Cases { get; set; }
        public int MinCases { get; set; }
        public int MaxCases { get; set; }

        public virtual Event Event { get; set; }
        public virtual Huffmodel25kmworldhexagon Grid { get; set; }
    }
}
