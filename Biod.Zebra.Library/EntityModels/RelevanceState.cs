//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Biod.Zebra.Library.EntityModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class RelevanceState
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RelevanceState()
        {
            this.Xtbl_Role_Disease_Relevance = new HashSet<Xtbl_Role_Disease_Relevance>();
            this.Xtbl_User_Disease_Relevance = new HashSet<Xtbl_User_Disease_Relevance>();
        }
    
        public int StateId { get; set; }
        public string Description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Xtbl_Role_Disease_Relevance> Xtbl_Role_Disease_Relevance { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Xtbl_User_Disease_Relevance> Xtbl_User_Disease_Relevance { get; set; }
    }
}