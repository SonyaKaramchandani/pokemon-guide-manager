using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class EventPriorities
    {
        public EventPriorities()
        {
            Event = new HashSet<Event>();
        }

        public int PriorityId { get; set; }
        public string PriorityTitle { get; set; }

        public virtual ICollection<Event> Event { get; set; }
    }
}
