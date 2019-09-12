namespace GeoNamesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GeoNames.Country")]
    public partial class Country
    {
        /* This is the real primary key in the database
         * but the geonameId is required to be marked here to 
         * establish the relationship via the Fluent API.  See:
         * http://www.entityframeworktutorial.net/code-first/configure-one-to-one-relationship-in-code-first.aspxk */
        [Required]
        [StringLength(2)]
        public string ISO { get; set; }

        [Required]
        [StringLength(3)]
        public string ISO3 { get; set; }

        [Required]
        public int ISOnumeric { get; set; }

        [StringLength(2)]
        public string FIPS { get; set; }

        [Column("country")]
        [Required]
        [StringLength(64)]
        public string country { get; set; }

        [StringLength(32)]
        public string capital { get; set; }

        public double? area { get; set; }

        [Required]
        public int population { get; set; }

        [Required]
        [StringLength(2)]
        public string continent { get; set; }

        [StringLength(3)]
        public string topLevelDomain { get; set; }

        [StringLength(3)]
        public string currencyCode { get; set; }

        [StringLength(16)]
        public string currencyName { get; set; }

        [StringLength(16)]
        public string phoneCode { get; set; }

        [StringLength(64)]
        public string postalCodeFormat { get; set; }

        [StringLength(256)]
        public string postalCodeRegex { get; set; }

        [StringLength(128)]
        public string languages { get; set; }

        [Required]
        [Key, ForeignKey("Place")]
        public int geonameid { get; set; }

        [StringLength(64)]
        public string neighbours { get; set; }

        [StringLength(2)]
        public string equivalentFIPScode { get; set; }

        public virtual Place Place { get; set; }
    }
}
