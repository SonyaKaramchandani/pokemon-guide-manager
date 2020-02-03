using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class GridStationBak
    {
        public string GridId { get; set; }
        public int StationId { get; set; }
        public decimal? Probability { get; set; }
        public DateTime ValidFromDate { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
