using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class Event
    {
        public Event()
        {
            EventDestinationAirportSpreadMd = new HashSet<EventDestinationAirportSpreadMd>();
            EventImportationRisksByGeonameSpreadMd = new HashSet<EventImportationRisksByGeonameSpreadMd>();
            EventSourceAirportSpreadMd = new HashSet<EventSourceAirportSpreadMd>();
            EventSourceGridSpreadMd = new HashSet<EventSourceGridSpreadMd>();
            UserEmailNotification = new HashSet<UserEmailNotification>();
            XtblArticleEvent = new HashSet<XtblArticleEvent>();
            XtblEventLocation = new HashSet<XtblEventLocation>();
            XtblEventLocationHistory = new HashSet<XtblEventLocationHistory>();
        }

        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int? PriorityId { get; set; }
        public bool? IsPublished { get; set; }
        public string Summary { get; set; }
        public string Notes { get; set; }
        public int DiseaseId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string EventMongoId { get; set; }
        public string LastUpdatedByUserName { get; set; }
        public bool IsLocalOnly { get; set; }
        public int SpeciesId { get; set; }

        public virtual Diseases Disease { get; set; }
        public virtual EventPriorities Priority { get; set; }
        public virtual Species Species { get; set; }
        public virtual EventExtensionSpreadMd EventExtensionSpreadMd { get; set; }
        public virtual ICollection<EventDestinationAirportSpreadMd> EventDestinationAirportSpreadMd { get; set; }
        public virtual ICollection<EventImportationRisksByGeonameSpreadMd> EventImportationRisksByGeonameSpreadMd { get; set; }
        public virtual ICollection<EventSourceAirportSpreadMd> EventSourceAirportSpreadMd { get; set; }
        public virtual ICollection<EventSourceGridSpreadMd> EventSourceGridSpreadMd { get; set; }
        public virtual ICollection<UserEmailNotification> UserEmailNotification { get; set; }
        public virtual ICollection<XtblArticleEvent> XtblArticleEvent { get; set; }
        public virtual ICollection<XtblEventLocation> XtblEventLocation { get; set; }
        public virtual ICollection<XtblEventLocationHistory> XtblEventLocationHistory { get; set; }
    }
}
