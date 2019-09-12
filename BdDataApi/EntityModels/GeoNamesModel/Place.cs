namespace GeoNamesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GeoNames.Place")]
    public partial class Place
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Place()
        {
            Shapes = new HashSet<Shape>();
            UserTags = new HashSet<UserTag>();
            AlternateNames = new HashSet<AlternateName>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required]
        [StringLength(256)]
        public string name { get; set; }

        [StringLength(256)]
        public string asciiname { get; set; }

        public string alternatenames { get; set; }

        [Required]
        [DataType("decimal(24, 20)")]
        public decimal latitude { get; set; }

        [Required]
        [DataType("decimal(24, 20)")]
        public decimal longitude { get; set; }

        [StringLength(1)]
        public string featureClass { get; set; }

        [StringLength(8)]
        public string featureCode { get; set; }

        [StringLength(2)]
        public string countryCode { get; set; }

        [StringLength(128)]
        public string cc2 { get; set; }

        [StringLength(20)]
        public string admin1Code { get; set; }

        [StringLength(80)]
        public string admin2Code { get; set; }

        [StringLength(20)]
        public string admin3Code { get; set; }

        [StringLength(20)]
        public string admin4Code { get; set; }

        [Required]
        public long population { get; set; }

        public int? elevation { get; set; }

        [Required]
        public int dem { get; set; }

        [StringLength(32)]
        public string timezone { get; set; }

        [Column(TypeName = "date")]
        public DateTime? modificationDate { get; set; }

        public virtual AdminCode AdminCode { get; set; }

        public virtual FeatureClass FeatureClassRef { get; set; }

        public virtual FeatureCode FeatureCodeRef { get; set; }

        public virtual Country Country { get; set; }


        public virtual TimeZone TimeZoneRef { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AlternateName> AlternateNames { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shape> Shapes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserTag> UserTags { get; set; }


        public string GetAdminCode()
        {
            string adminCode = this.countryCode + '.' + this.admin1Code;
            if (null != this.admin2Code  &&  this.admin2Code.Length > 0)
                adminCode += '.' + this.admin2Code;
            return adminCode;
        }

    }
}
