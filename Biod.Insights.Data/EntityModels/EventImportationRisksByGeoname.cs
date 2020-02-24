using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class EventImportationRisksByGeoname
    {
        public int EventId { get; set; }
        public int GeonameId { get; set; }
        public int LocalSpread { get; set; }
        public decimal? MinProb { get; set; }
        public decimal? MaxProb { get; set; }
        public decimal? MinVolume { get; set; }
        public decimal? MaxVolume { get; set; }

        public virtual Event Event { get; set; }
        public virtual Geonames Geoname { get; set; }
    }
}
