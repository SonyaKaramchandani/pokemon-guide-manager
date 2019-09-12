//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Biod.Surveillance.Zebra.SyncConsole.EntityModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProcessedArticle
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProcessedArticle()
        {
            this.Xtbl_Article_Location_Disease = new HashSet<Xtbl_Article_Location_Disease>();
            this.Xtbl_RelatedArticles = new HashSet<Xtbl_RelatedArticles>();
            this.Events = new HashSet<Event>();
            this.Geonames = new HashSet<Geoname>();
            this.SuggestedEvents = new HashSet<SuggestedEvent>();
        }
    
        public string ArticleId { get; set; }
        public string ArticleTitle { get; set; }
        public System.DateTime SystemLastModifiedDate { get; set; }
        public Nullable<decimal> CertaintyScore { get; set; }
        public Nullable<int> ArticleFeedId { get; set; }
        public string FeedURL { get; set; }
        public string FeedSourceId { get; set; }
        public System.DateTime FeedPublishedDate { get; set; }
        public Nullable<int> HamTypeId { get; set; }
        public string OriginalSourceURL { get; set; }
        public Nullable<bool> IsCompleted { get; set; }
        public Nullable<decimal> SimilarClusterId { get; set; }
        public string OriginalLanguage { get; set; }
        public Nullable<System.DateTime> UserLastModifiedDate { get; set; }
        public string LastUpdatedByUserName { get; set; }
        public string Notes { get; set; }
        public string ArticleBody { get; set; }
        public Nullable<bool> IsRead { get; set; }
        public Nullable<bool> IsImportant { get; set; }
    
        public virtual ArticleFeed ArticleFeed { get; set; }
        public virtual HamType HamType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Xtbl_Article_Location_Disease> Xtbl_Article_Location_Disease { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Xtbl_RelatedArticles> Xtbl_RelatedArticles { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Events { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Geoname> Geonames { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuggestedEvent> SuggestedEvents { get; set; }
    }
}
