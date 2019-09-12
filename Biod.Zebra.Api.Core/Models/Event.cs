using System;
using System.Collections.Generic;

namespace Biod.Zebra.Api.Core.Models
{
    public partial class Event
    {
        public Event()
        {
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

        public ICollection<XtblArticleEvent> XtblArticleEvent { get; set; }
        public ICollection<XtblEventLocation> XtblEventLocation { get; set; }
        public ICollection<XtblEventReason> XtblEventReason { get; set; }
    }
}
