namespace GeoNamesModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GeoNames.Shape")]
    public partial class Shape
    {
        [Key, ForeignKey("Place")]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int geonameid { get; set; }

        [Key]
        [Column(Order = 1)]
        public string geoJSON { get; set; }

        public virtual Place Place { get; set; }
    }
}
