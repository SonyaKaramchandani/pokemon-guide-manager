using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class EventSourceGrid
    {
        public int EventId { get; set; }
        public string GridId { get; set; }

        public Event Event { get; set; }
    }
}
