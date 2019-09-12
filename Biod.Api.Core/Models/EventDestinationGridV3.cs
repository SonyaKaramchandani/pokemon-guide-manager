using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class EventDestinationGridV3
    {
        public int EventId { get; set; }
        public string GridId { get; set; }

        public Event Event { get; set; }
        public Huffmodel25kmworldhexagon Grid { get; set; }
    }
}
