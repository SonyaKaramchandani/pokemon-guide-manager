using System;
using System.Collections.Generic;

namespace Biod.Api.Core.Models
{
    public partial class Event
    {
        public Event()
        {
            EventDestinationAirport = new HashSet<EventDestinationAirport>();
            EventDestinationGridV3 = new HashSet<EventDestinationGridV3>();
            EventSourceAirport = new HashSet<EventSourceAirport>();
            EventSourceGrid = new HashSet<EventSourceGrid>();
            XtblArticleEvent = new HashSet<XtblArticleEvent>();
            XtblEventLocation = new HashSet<XtblEventLocation>();
            XtblEventReason = new HashSet<XtblEventReason>();
        }

        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int? PriorityId { get; set; }
        public bool? IsPublished { get; set; }
        public string Summary { get; set; }
        public string Notes { get; set; }
        public int? DiseaseId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EventMongoId { get; set; }
        public string LastUpdatedByUserName { get; set; }
        public bool IsLocalOnly { get; set; }
        public bool? HasOutlookReport { get; set; }

        public EventPriorities Priority { get; set; }
        public ICollection<EventDestinationAirport> EventDestinationAirport { get; set; }
        public ICollection<EventDestinationGridV3> EventDestinationGridV3 { get; set; }
        public ICollection<EventSourceAirport> EventSourceAirport { get; set; }
        public ICollection<EventSourceGrid> EventSourceGrid { get; set; }
        public ICollection<XtblArticleEvent> XtblArticleEvent { get; set; }
        public ICollection<XtblEventLocation> XtblEventLocation { get; set; }
        public ICollection<XtblEventReason> XtblEventReason { get; set; }
    }
}
