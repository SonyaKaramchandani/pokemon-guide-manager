using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class EventPrevalenceHistory
    {
        public int EventId { get; set; }
        public double MinPrevelance { get; set; }
        public double MaxPrevelance { get; set; }
        public int EventMonth { get; set; }

        public virtual Event Event { get; set; }
    }
}
