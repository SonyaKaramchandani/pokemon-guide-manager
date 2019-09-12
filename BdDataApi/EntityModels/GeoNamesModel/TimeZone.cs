namespace GeoNamesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GeoNames.TimeZone")]
    public partial class TimeZone
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TimeZone()
        {
            Places = new HashSet<Place>();
        }

        [Required]
        [StringLength(2)]
        public string countryCode { get; set; }

        [Required]
        [StringLength(32)]
        public string id { get; set; }

        [Required]
        public double gmtOffset { get; set; }

        [Required]
        public double dstOffset { get; set; }

        [Required]
        public double rawOffset { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Place> Places { get; set; }
    }
}
