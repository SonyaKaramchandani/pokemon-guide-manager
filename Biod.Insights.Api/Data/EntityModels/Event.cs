using System;
using System.Collections.Generic;

namespace Biod.Insights.Api.Data.EntityModels
{
    public partial class Event
    {
        public Event()
        {
            EventDestinationAirport = new HashSet<EventDestinationAirport>();
            EventDestinationAirportHistory = new HashSet<EventDestinationAirportHistory>();
            EventDestinationGridHistory = new HashSet<EventDestinationGridHistory>();
            EventDestinationGridV3 = new HashSet<EventDestinationGridV3>();
            EventImportationRisksByUser = new HashSet<EventImportationRisksByUser>();
            EventImportationRisksByUserHistory = new HashSet<EventImportationRisksByUserHistory>();
            EventSourceAirport = new HashSet<EventSourceAirport>();
            UserEmailNotification = new HashSet<UserEmailNotification>();
            XtblArticleEvent = new HashSet<XtblArticleEvent>();
            XtblEventLocation = new HashSet<XtblEventLocation>();
            XtblEventLocationHistory = new HashSet<XtblEventLocationHistory>();
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
        public int SpeciesId { get; set; }

        public virtual EventPriorities Priority { get; set; }
        public virtual Species Species { get; set; }
        public virtual EventPrevalence EventPrevalence { get; set; }
        public virtual EventPrevalenceHistory EventPrevalenceHistory { get; set; }
        public virtual ICollection<EventDestinationAirport> EventDestinationAirport { get; set; }
        public virtual ICollection<EventDestinationAirportHistory> EventDestinationAirportHistory { get; set; }
        public virtual ICollection<EventDestinationGridHistory> EventDestinationGridHistory { get; set; }
        public virtual ICollection<EventDestinationGridV3> EventDestinationGridV3 { get; set; }
        public virtual ICollection<EventImportationRisksByUser> EventImportationRisksByUser { get; set; }
        public virtual ICollection<EventImportationRisksByUserHistory> EventImportationRisksByUserHistory { get; set; }
        public virtual ICollection<EventSourceAirport> EventSourceAirport { get; set; }
        public virtual ICollection<UserEmailNotification> UserEmailNotification { get; set; }
        public virtual ICollection<XtblArticleEvent> XtblArticleEvent { get; set; }
        public virtual ICollection<XtblEventLocation> XtblEventLocation { get; set; }
        public virtual ICollection<XtblEventLocationHistory> XtblEventLocationHistory { get; set; }
        public virtual ICollection<XtblEventReason> XtblEventReason { get; set; }
    }
}
