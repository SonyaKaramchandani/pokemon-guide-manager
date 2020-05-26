using System;

namespace Biod.Insights.Data.EntityModels
{
    public partial class UvwLastEventNestedLocation
    {
        public int EventId { get; set; }
        public int GeonameId { get; set; }
        public DateTime EventDate { get; set; }
        public int? SuspCases { get; set; }
        public int? ConfCases { get; set; }
        public int? RepCases { get; set; }
        public int? Deaths { get; set; }
        public int? NewSuspCases { get; set; }
        public int? NewConfCases { get; set; }
        public int? NewRepCases { get; set; }
        public int? NewDeaths { get; set; }
    }
}
