//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Biod.Surveillance.SyncConsole.Client.EntityModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class place_Geonames
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public place_Geonames()
        {
            this.surveillance_Xtbl_Event_Location = new HashSet<surveillance_Xtbl_Event_Location>();
        }
    
        public int GeonameId { get; set; }
        public string Name { get; set; }
        public Nullable<int> LocationType { get; set; }
        public Nullable<int> Admin1GeonameId { get; set; }
        public Nullable<int> CountryGeonameId { get; set; }
        public string DisplayName { get; set; }
        public string Alternatenames { get; set; }
        public System.DateTime ModificationDate { get; set; }
        public string FeatureCode { get; set; }
        public string CountryName { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
        public Nullable<long> Population { get; set; }
        public Nullable<int> SearchSeq2 { get; set; }
        public System.Data.Entity.Spatial.DbGeography Shape { get; set; }
        public Nullable<decimal> LatPopWeighted { get; set; }
        public Nullable<decimal> LongPopWeighted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<surveillance_Xtbl_Event_Location> surveillance_Xtbl_Event_Location { get; set; }
    }
}