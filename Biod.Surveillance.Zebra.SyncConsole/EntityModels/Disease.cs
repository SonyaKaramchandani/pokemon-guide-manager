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
    
    public partial class Disease
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Disease()
        {
            this.SuggestedEvents = new HashSet<SuggestedEvent>();
            this.Xtbl_Article_Location_Disease = new HashSet<Xtbl_Article_Location_Disease>();
        }
    
        public int DiseaseId { get; set; }
        public string DiseaseName { get; set; }
        public string DiseaseType { get; set; }
        public Nullable<System.DateTime> LastModified { get; set; }
        public Nullable<int> ParentDiseaseId { get; set; }
        public string Pronunciation { get; set; }
        public string SeverityLevel { get; set; }
        public Nullable<bool> IsChronic { get; set; }
        public string TreatmentAvailable { get; set; }
        public string BiosecurityRisk { get; set; }
        public Nullable<bool> IsZoonotic { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuggestedEvent> SuggestedEvents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Xtbl_Article_Location_Disease> Xtbl_Article_Location_Disease { get; set; }
    }
}
