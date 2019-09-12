using System;
using System.Collections.Generic;

namespace Biod.Zebra.Api.Core.Models
{
    public partial class XtblEventLocation
    {
        public int EventId { get; set; }
        public int GeonameId { get; set; }
        public DateTime EventDate { get; set; }
        public int? SuspCases { get; set; }
        public int? ConfCases { get; set; }
        public int? RepCases { get; set; }
        public int? Deaths { get; set; }

        public Event Event { get; set; }
        public Geonames Geoname { get; set; }
    }
}
