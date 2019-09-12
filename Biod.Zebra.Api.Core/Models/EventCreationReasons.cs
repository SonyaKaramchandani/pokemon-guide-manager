using System;
using System.Collections.Generic;

namespace Biod.Zebra.Api.Core.Models
{
    public partial class EventCreationReasons
    {
        public EventCreationReasons()
        {
            XtblEventReason = new HashSet<XtblEventReason>();
        }

        public int ReasonId { get; set; }
        public string ReasonName { get; set; }

        public ICollection<XtblEventReason> XtblEventReason { get; set; }
    }
}
