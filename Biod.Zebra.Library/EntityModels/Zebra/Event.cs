//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Biod.Zebra.Library.EntityModels.Zebra
{
    using System;
    using System.Collections.Generic;
    
    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            this.Xtbl_Event_Location = new HashSet<Xtbl_Event_Location>();
            this.ProcessedArticles = new HashSet<ProcessedArticle>();
            this.EventCreationReasons = new HashSet<EventCreationReason>();
            this.UserEmailNotifications = new HashSet<UserEmailNotification>();
            this.Xtbl_Event_Location_history = new HashSet<Xtbl_Event_Location_history>();
            this.EventSourceAirportSpreadMds = new HashSet<EventSourceAirportSpreadMd>();
            this.EventSourceGridSpreadMds = new HashSet<EventSourceGridSpreadMd>();
            this.EventLocations = new HashSet<EventLocation>();
            this.EventNestedLocations = new HashSet<EventNestedLocation>();
        }
    
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
        public Nullable<int> PriorityId { get; set; }
        public Nullable<bool> IsPublished { get; set; }
        public string Summary { get; set; }
        public string Notes { get; set; }
        public int DiseaseId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string EventMongoId { get; set; }
        public string LastUpdatedByUserName { get; set; }
        public bool IsLocalOnly { get; set; }
        public int SpeciesId { get; set; }
    
        public virtual EventPriority EventPriority { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Xtbl_Event_Location> Xtbl_Event_Location { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProcessedArticle> ProcessedArticles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventCreationReason> EventCreationReasons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserEmailNotification> UserEmailNotifications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Xtbl_Event_Location_history> Xtbl_Event_Location_history { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventSourceAirportSpreadMd> EventSourceAirportSpreadMds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventSourceGridSpreadMd> EventSourceGridSpreadMds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventLocation> EventLocations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventNestedLocation> EventNestedLocations { get; set; }
    }
}
