using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.Models
{
    public partial class EventCreationReasons
    {
        public EventCreationReasons()
        {
            XtblEventReason = new HashSet<XtblEventReason>();
        }

        public int ReasonId { get; set; }
        public string ReasonName { get; set; }

        public virtual ICollection<XtblEventReason> XtblEventReason { get; set; }
    }
}
