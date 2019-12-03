using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class XtblEventLocationHistory
    {
        public int EventId { get; set; }
        public int EventDateType { get; set; }
        public int GeonameId { get; set; }
        public DateTime EventDate { get; set; }
        public int? SuspCases { get; set; }
        public int? ConfCases { get; set; }
        public int? RepCases { get; set; }
        public int? Deaths { get; set; }

        public virtual Event Event { get; set; }
        public virtual ActiveGeonames Geoname { get; set; }
    }
}
