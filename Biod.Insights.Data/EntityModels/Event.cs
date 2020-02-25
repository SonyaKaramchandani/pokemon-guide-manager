using System;
using System.Collections.Generic;

namespace Biod.Insights.Data.EntityModels
{
    public partial class Event
    {
        public Event()
        {
            EventDestinationAirport = new HashSet<EventDestinationAirport>();
            EventImportationRisksByGeoname = new HashSet<EventImportationRisksByGeoname>();
            EventSourceAirport = new HashSet<EventSourceAirport>();
            XtblArticleEvent = new HashSet<XtblArticleEvent>();
            XtblEventLocation = new HashSet<XtblEventLocation>();
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
        public virtual EventExtension EventExtension { get; set; }
        public virtual ICollection<EventDestinationAirport> EventDestinationAirport { get; set; }
        public virtual ICollection<EventImportationRisksByGeoname> EventImportationRisksByGeoname { get; set; }
        public virtual ICollection<EventSourceAirport> EventSourceAirport { get; set; }
        public virtual ICollection<XtblArticleEvent> XtblArticleEvent { get; set; }
        public virtual ICollection<XtblEventLocation> XtblEventLocation { get; set; }
    }
}
