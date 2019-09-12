using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class XtblEventReason
    {
        public int EventId { get; set; }
        public int ReasonId { get; set; }

        public Event Event { get; set; }
        public EventCreationReasons Reason { get; set; }
    }
}
