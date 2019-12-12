using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class XtblEventReason
    {
        public int EventId { get; set; }
        public int ReasonId { get; set; }

        public virtual Event Event { get; set; }
        public virtual EventCreationReasons Reason { get; set; }
    }
}
