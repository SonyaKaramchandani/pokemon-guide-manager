namespace GeoNamesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GeoNames.AlternateNames")]
    public partial class AlternateName
    {
        [Required]
        [Key]
        public int id { get; set; }

        [Required]
        [ForeignKey("Place")]
        public int geonameid { get; set; }

        [Required]
        [StringLength(7)]
        public string isolanguage { get; set; }

        [Required]
        [StringLength(256)]
        public string alternateName { get; set; }

        public bool isPreferredName { get; set; }

        public bool isShortName { get; set; }

        public bool isColloquial { get; set; }

        public bool isHistoric { get; set; }

        public virtual Place Place { get; set; }
    }
}
