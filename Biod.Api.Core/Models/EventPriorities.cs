using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class EventPriorities
    {
        public EventPriorities()
        {
            Event = new HashSet<Event>();
        }

        public int PriorityId { get; set; }
        public string PriorityTitle { get; set; }

        public ICollection<Event> Event { get; set; }
    }
}
