namespace GeoNamesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GeoNames.FeatureCode")]
    public partial class FeatureCode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FeatureCode()
        {
            Places = new HashSet<Place>();
        }

        [Key]
        [StringLength(8)]
        public string code { get; set; }

        [Column("class")]
        [Required]
        [StringLength(1)]
        public string @class { get; set; }

        [Required]
        [StringLength(64)]
        public string name { get; set; }

        [StringLength(256)]
        public string description { get; set; }

        public virtual FeatureClass FeatureClass { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Place> Places { get; set; }
    }
}
