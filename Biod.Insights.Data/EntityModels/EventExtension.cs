using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class EventExtension
    {
        public int EventId { get; set; }
        public int? AirportsDestinationVolume { get; set; }
        public decimal? MinExportationProbabilityViaAirports { get; set; }
        public decimal? MaxExportationProbabilityViaAirports { get; set; }
        public decimal? MinExportationVolumeViaAirports { get; set; }
        public decimal? MaxExportationVolumeViaAirports { get; set; }

        public virtual Event Event { get; set; }
    }
}
