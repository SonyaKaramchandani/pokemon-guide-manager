using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class EventImportationRisksByUserHistory
    {
        public string UserId { get; set; }
        public int LocalSpread { get; set; }
        public int EventId { get; set; }
        public decimal? MinProb { get; set; }
        public decimal? MaxProb { get; set; }
        public decimal? MinVolume { get; set; }
        public decimal? MaxVolume { get; set; }

        public virtual Event Event { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
