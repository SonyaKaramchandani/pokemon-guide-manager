using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class EventDestinationGridV3
    {
        public int EventId { get; set; }
        public string GridId { get; set; }

        public virtual Event Event { get; set; }
        public virtual Huffmodel25kmworldhexagon Grid { get; set; }
    }
}
