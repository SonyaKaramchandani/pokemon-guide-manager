using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class XtblEventLocationTransLog
    {
        public int LogId { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public string Action { get; set; }
        public int EventId { get; set; }
        public int GeonameId { get; set; }
        public DateTime? EventDate { get; set; }
        public int? SuspCases { get; set; }
        public int? ConfCases { get; set; }
        public int? RepCases { get; set; }
        public int? Deaths { get; set; }
    }
}
